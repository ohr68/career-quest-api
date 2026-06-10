using System.Text.Json;
using Anthropic;
using Anthropic.Models.Messages;
using CareerQuest.Common.Domain.Abstractions;
using CareerQuest.Modules.Players.Application.Abstractions.Intelligence;
using CareerQuest.Modules.Players.Domain.Intelligence;
using CareerQuest.Modules.Players.Domain.Players;
using Microsoft.Extensions.Options;

namespace CareerQuest.Modules.Players.Infrastructure.Intelligence;

internal sealed class ClaudeActivityClassifier(
    AnthropicClient client,
    IOptions<AnthropicOptions> options) : IActivityClassifier
{
    private const string SystemPrompt =
        """
        You classify a developer's career activity for a gamified progression tracker.
        Given a free-text description of what the player did, decide:
        - action: a short snake_case label (e.g. "open_source_contribution", "blog_post", "networking")
        - difficulty: Easy (routine, < 1h), Medium (substantial, multi-hour), High (major, multi-day or high-impact)
        - suggestedXp: 10-50 base XP proportional to effort, BEFORE difficulty multipliers
        - looksImplausible: true if the claim is vague, unverifiable bragging, or inflated
        Be conservative: when unsure, pick the lower difficulty.                               
        """;

    private static readonly Dictionary<string, JsonElement> Schema = new()
    {
        ["type"] = JsonSerializer.SerializeToElement("object"),
        ["properties"] = JsonSerializer.SerializeToElement(new
        {
            action = new { type = "string" },
            difficulty = new { type = "string", @enum = new[] { "Easy", "Medium", "High" } },
            suggestedXp = new { type = "integer" },
            looksImplausible = new { type = "boolean" },
            reasoning = new { type = "string" },
        }),
        ["required"] = JsonSerializer.SerializeToElement(
            new[] { "action", "difficulty", "suggestedXp", "looksImplausible", "reasoning" }),
        ["additionalProperties"] = JsonSerializer.SerializeToElement(false),
    };

    public async Task<Result<ActivityClassification>> ClassifyAsync(string description,
        CancellationToken cancellationToken = default)
    {
        Message response;
        try
        {
            response = await client.Messages.Create(new MessageCreateParams
            {
                Model = options.Value.ClassifierModel,
                MaxTokens = 1024,
                System = new List<TextBlockParam>
                {
                    new() { Text = SystemPrompt, CacheControl = new CacheControlEphemeral() },
                },
                OutputConfig = new OutputConfig
                {
                    Format = new JsonOutputFormat { Schema = Schema },
                },
                Messages = [new MessageParam { Role = Role.User, Content = description }],
            }, cancellationToken);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Result.Failure<ActivityClassification>(IntelligenceErrors.Unavailable);
        }

        string? json = response.Content
            .Select(b => b.Value)
            .OfType<TextBlock>()
            .FirstOrDefault()?.Text;

        if (json is null)
        {
            return Result.Failure<ActivityClassification>(IntelligenceErrors.InvalidResponse);
        }

        ClassificationPayload? payload = JsonSerializer.Deserialize<ClassificationPayload>(
            json, JsonSerializerOptions.Web);

        return payload is null || !Enum.TryParse(payload.Difficulty, out DifficultyModifier modifier)
            ? Result.Failure<ActivityClassification>(IntelligenceErrors.InvalidResponse)
            : new ActivityClassification(
                payload.Action,
                modifier,
                Math.Clamp(payload.SuggestedXp, 1, 50),
                payload.LooksImplausible,
                payload.Reasoning);
    }

    private sealed record ClassificationPayload(
        string Action,
        string Difficulty,
        int SuggestedXp,
        bool LooksImplausible,
        string Reasoning);
}

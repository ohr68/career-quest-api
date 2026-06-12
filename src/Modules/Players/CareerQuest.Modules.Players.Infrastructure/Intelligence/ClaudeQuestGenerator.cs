using System.Text.Json;
using Anthropic;
using Anthropic.Models.Messages;
using CareerQuest.Common.Domain.Abstractions;
using CareerQuest.Modules.Players.Application.Abstractions.Intelligence;
using CareerQuest.Modules.Players.Domain.Intelligence;
using CareerQuest.Modules.Players.Domain.Players;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CareerQuest.Modules.Players.Infrastructure.Intelligence;

internal sealed class ClaudeQuestGenerator(
    AnthropicClient client,
    IOptions<AnthropicOptions> options,
    ILogger<ClaudeQuestGenerator> logger) : IQuestGenerator
{
    private const string SystemPrompt =
        """
        You generate weekly career quests for a gamified developer progression tracker.
        Given a player's career stage, level, classes, specializations and recent activity,
        produce quests that are:
        - concrete and verifiable (a clear "done" condition, not "improve your skills")
        - completable within one week alongside a full-time job
        - matched to the player's classes/specializations and appropriate for their career stage
        - different from the player's recent actions (push them to vary, not repeat)
        Difficulty: Easy (a focused evening), Medium (several evenings), High (most of the week).
        XP rewards must be between 20 and 60, proportional to difficulty.
        """;

    private static readonly Dictionary<string, JsonElement> Schema = new()
    {
        ["type"] = JsonSerializer.SerializeToElement("object"),
        ["properties"] = JsonSerializer.SerializeToElement(new
        {
            quests = new
            {
                type = "array",
                items = new
                {
                    type = "object",
                    properties = new
                    {
                        title = new { type = "string" },
                        description = new { type = "string" },
                        xpReward = new { type = "integer" },
                        difficulty = new { type = "string", @enum = new[] { "Easy", "Medium", "High" } },
                    },
                    required = new[] { "title", "description", "xpReward", "difficulty" },
                    additionalProperties = false,
                },
            },
        }),
        ["required"] = JsonSerializer.SerializeToElement(new[] { "quests" }),
        ["additionalProperties"] = JsonSerializer.SerializeToElement(false),
    };

    public async Task<Result<IReadOnlyCollection<QuestDraft>>> GenerateAsync(
        PlayerQuestContext context,
        CancellationToken cancellationToken = default)
    {
        Message response;
        try
        {
            response = await client.Messages.Create(new MessageCreateParams
            {
                Model = options.Value.GenerationModel,
                MaxTokens = 2048,
                System = new List<TextBlockParam>
                {
                    new() { Text = SystemPrompt, CacheControl = new CacheControlEphemeral() },
                },
                OutputConfig = new OutputConfig
                {
                    Format = new JsonOutputFormat { Schema = Schema },
                },
                Messages =
                [
                    new MessageParam
                    {
                        Role = Role.User,
                        Content = $"""
                                   Generate exactly 3 weekly quests for this player:
                                   {JsonSerializer.Serialize(context, JsonSerializerOptions.Web)}
                                   """,
                    },
                ],
            }, cancellationToken);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Quest generation request failed");

            return Result.Failure<IReadOnlyCollection<QuestDraft>>(IntelligenceErrors.Unavailable);
        }

        string? json = response.Content
            .Select(b => b.Value)
            .OfType<TextBlock>()
            .FirstOrDefault()?.Text;

        if (json is null)
        {
            return Result.Failure<IReadOnlyCollection<QuestDraft>>(IntelligenceErrors.InvalidResponse);
        }

        QuestsPayload? payload = JsonSerializer.Deserialize<QuestsPayload>(json, JsonSerializerOptions.Web);

        if (payload is null || payload.Quests.Count == 0)
        {
            return Result.Failure<IReadOnlyCollection<QuestDraft>>(IntelligenceErrors.InvalidResponse);
        }

        List<QuestDraft> drafts = [];

        foreach (QuestPayload quest in payload.Quests)
        {
            if (!Enum.TryParse(quest.Difficulty, out DifficultyModifier difficulty))
            {
                return Result.Failure<IReadOnlyCollection<QuestDraft>>(IntelligenceErrors.InvalidResponse);
            }

            drafts.Add(new QuestDraft(
                quest.Title,
                quest.Description,
                Math.Clamp(quest.XpReward, 20, 60),
                difficulty));
        }

        return drafts;
    }

    private sealed record QuestsPayload(IReadOnlyList<QuestPayload> Quests);

    private sealed record QuestPayload(
        string Title,
        string Description,
        int XpReward,
        string Difficulty);
}

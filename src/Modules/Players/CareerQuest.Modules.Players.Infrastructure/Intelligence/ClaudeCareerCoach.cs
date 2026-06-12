using System.Text.Json;
using Anthropic;
using Anthropic.Models.Messages;
using CareerQuest.Common.Domain.Abstractions;
using CareerQuest.Modules.Players.Application.Abstractions.Intelligence;
using CareerQuest.Modules.Players.Domain.Intelligence;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CareerQuest.Modules.Players.Infrastructure.Intelligence;

internal sealed class ClaudeCareerCoach(
    AnthropicClient client,
    IOptions<AnthropicOptions> options,
    ILogger<ClaudeCareerCoach> logger) : ICareerCoach
{
    private const string SystemPrompt =
        """
        You are a pragmatic career coach for software developers, embedded in a gamified
        career progression tracker. Given a player's profile, level, streak, classes,
        specializations and recent activity, write a short coaching note in markdown:
        - open with one sentence acknowledging something specific from their data
        - give exactly 3 specific, actionable recommendations for the coming weeks,
          each tied to their career stage and specializations
        - close with one sentence on what reaching the next career stage would require
        No generic advice ("network more", "keep learning"). Address the player by name.
        Keep the whole note under 250 words.
        """;

    public async Task<Result<string>> AdviseAsync(
        PlayerCoachingContext context,
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
                Messages =
                [
                    new MessageParam
                    {
                        Role = Role.User,
                        Content = JsonSerializer.Serialize(context, JsonSerializerOptions.Web),
                    },
                ],
            }, cancellationToken);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Career coaching request failed");

            return Result.Failure<string>(IntelligenceErrors.Unavailable);
        }

        string? advice = response.Content
            .Select(b => b.Value)
            .OfType<TextBlock>()
            .FirstOrDefault()?.Text;

        return advice is null
            ? Result.Failure<string>(IntelligenceErrors.InvalidResponse)
            : advice;
    }
}

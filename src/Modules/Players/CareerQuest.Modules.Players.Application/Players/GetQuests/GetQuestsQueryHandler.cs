using CareerQuest.Common.Application.Messaging;
using CareerQuest.Common.Domain.Abstractions;
using CareerQuest.Modules.Players.Domain.Players;

namespace CareerQuest.Modules.Players.Application.Players.GetQuests;

internal sealed class GetQuestsQueryHandler(
    IPlayerRepository playerRepository)
    : IQueryHandler<GetQuestsQuery, IReadOnlyCollection<QuestResponse>>
{
    public async Task<Result<IReadOnlyCollection<QuestResponse>>> Handle(
        GetQuestsQuery request, CancellationToken cancellationToken)
    {
        Player? player = await playerRepository.GetWithQuestsAsync(request.PlayerId, cancellationToken);

        if (player is null)
        {
            return Result.Failure<IReadOnlyCollection<QuestResponse>>(
                PlayerErrors.NotFound(request.PlayerId));
        }

        return player.Quests
            .OrderByDescending(q => q.ExpiresAtUtc)
            .Select(q => new QuestResponse(
                q.Id,
                q.Title,
                q.Description,
                q.XpReward,
                q.Difficulty,
                q.ExpiresAtUtc,
                q.CompletedAtUtc))
            .ToList();
    }
}

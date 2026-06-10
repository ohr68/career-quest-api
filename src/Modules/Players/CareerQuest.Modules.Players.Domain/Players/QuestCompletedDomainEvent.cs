using CareerQuest.Common.Domain.Abstractions;

namespace CareerQuest.Modules.Players.Domain.Players;

public sealed class QuestCompletedDomainEvent(
    Guid playerId,
    Guid questId,
    int xpReward
) : DomainEvent
{
    public Guid PlayerId { get; init; } = playerId;
    public Guid QuestId { get; init; } = questId;
    public int XpReward { get; init; } = xpReward;
}

using CareerQuest.Common.Domain.Abstractions;

namespace CareerQuest.Modules.Players.Domain.Players;

public sealed class PlayerLeveledUpDomainEvent(Guid playerId, int currentLevel) : DomainEvent
{
    public Guid PlayerId { get; init; } = playerId;
    public int CurrentLevel { get; init; } = currentLevel;
}

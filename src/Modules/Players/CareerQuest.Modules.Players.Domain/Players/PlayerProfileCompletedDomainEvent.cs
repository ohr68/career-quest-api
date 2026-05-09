using CareerQuest.Common.Domain.Abstractions;

namespace CareerQuest.Modules.Players.Domain.Players;

public sealed class PlayerProfileCompletedDomainEvent(Guid playerId) : DomainEvent
{
    public Guid PlayerId { get; init; } = playerId;
}

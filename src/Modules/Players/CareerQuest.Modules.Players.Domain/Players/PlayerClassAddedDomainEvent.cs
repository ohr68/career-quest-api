using CareerQuest.Common.Domain.Abstractions;

namespace CareerQuest.Modules.Players.Domain.Players;

public sealed class PlayerClassAddedDomainEvent(
    Guid playerId,
    PlayerClassType playerClassType) : DomainEvent
{
    public Guid PlayerId { get; init; } = playerId;
    public PlayerClassType PlayerClassType { get; init; } = playerClassType;
}

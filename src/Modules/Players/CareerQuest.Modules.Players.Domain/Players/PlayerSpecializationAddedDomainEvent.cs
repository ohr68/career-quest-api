using CareerQuest.Common.Domain.Abstractions;

namespace CareerQuest.Modules.Players.Domain.Players;

public sealed class PlayerSpecializationAddedDomainEvent(
    Guid playerId,
    PlayerSpecializationType playerSpecializationType) : DomainEvent
{
    public Guid PlayerId { get; init; } = playerId;
    public PlayerSpecializationType PlayerSpecializationType { get; init; } = playerSpecializationType;
}

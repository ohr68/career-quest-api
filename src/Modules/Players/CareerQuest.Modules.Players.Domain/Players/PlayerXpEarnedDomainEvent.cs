using CareerQuest.Common.Domain.Abstractions;

namespace CareerQuest.Modules.Players.Domain.Players;

public sealed class PlayerXpEarnedDomainEvent(
    Guid playerId,
    int finalAmount,
    int currentLevel,
    int totalXp) : DomainEvent
{
    public Guid PlayerId { get; init; } = playerId;
    public int FinalAmount { get; init; } = finalAmount;
    public int CurrentLevel { get; init; } = currentLevel;
    public int TotalXp { get; init; } = totalXp;
}

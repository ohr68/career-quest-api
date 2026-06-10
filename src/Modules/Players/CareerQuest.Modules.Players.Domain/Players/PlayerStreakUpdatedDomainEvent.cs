using CareerQuest.Common.Domain.Abstractions;

namespace CareerQuest.Modules.Players.Domain.Players;

public sealed class PlayerStreakUpdatedDomainEvent(
    Guid playerId,
    int currentDays,
    int longestDays,
    decimal currentMultiplier) : DomainEvent
{
    public Guid PlayerId { get; init; } = playerId;
    public int CurrentDays { get; init; } = currentDays;
    public int LongestDays { get; init; } = longestDays;
    public decimal CurrentMultiplier { get; init; } = currentMultiplier;
}

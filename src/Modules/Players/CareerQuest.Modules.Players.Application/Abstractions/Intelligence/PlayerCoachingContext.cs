using CareerQuest.Modules.Players.Domain.Players;

namespace CareerQuest.Modules.Players.Application.Abstractions.Intelligence;

public sealed record PlayerCoachingContext(
    string DisplayName,
    string? Headline,
    CareerStage CareerStage,
    int CurrentLevel,
    int CurrentStreakDays,
    IReadOnlyCollection<PlayerClassType> Specializations,
    IReadOnlyCollection<string> RecentActions);

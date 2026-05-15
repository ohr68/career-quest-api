using CareerQuest.Modules.Players.Domain.Players;

namespace CareerQuest.Modules.Players.Application.Players.GetPlayer;

public sealed record PlayerResponse(
    Guid PlayerId,
    string DisplayName,
    string Email,
    Uri? AvatarUrl,
    string? Headline,
    CareerStage CareerStage,
    DateTime JoinedAtUtc,
    DateTime LastActiveAtUtc,
    PlayerProgression? Progression,
    PlayerStatistics? Statistics,
    PlayerStreak? Streak,
    IReadOnlyCollection<PlayerClassType> Classes,
    IReadOnlyCollection<PlayerSpecializationType> Specializations)
{
    public bool IsProfileCompleted =>
        Progression is not null &&
        Statistics is not null &&
        Streak is not null &&
        Classes.Count > 0 &&
        Specializations.Count > 0;
}

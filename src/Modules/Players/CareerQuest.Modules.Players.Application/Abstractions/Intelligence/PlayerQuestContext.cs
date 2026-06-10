using CareerQuest.Modules.Players.Domain.Players;

namespace CareerQuest.Modules.Players.Application.Abstractions.Intelligence;

public sealed record PlayerQuestContext(
    CareerStage CareerStage,
    int CurrentLevel,
    IReadOnlyCollection<PlayerClassType> Classes,
    IReadOnlyCollection<PlayerSpecializationType> Specializations,
    IReadOnlyCollection<string> RecentActions);

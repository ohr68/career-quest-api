using CareerQuest.Modules.Players.Domain.Players;

namespace CareerQuest.Modules.Players.Application.Abstractions.Intelligence;

public sealed record ActivityClassification(
    string Action,
    DifficultyModifier SuggestedModifier,
    int SuggestedXp,
    bool LooksImplausible,
    string Reasoning);

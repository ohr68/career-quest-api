using CareerQuest.Modules.Players.Domain.Players;

namespace CareerQuest.Modules.Players.Application.Abstractions.Intelligence;

public sealed record QuestDraft(
    string Title,
    string Description,
    int XpReward,
    DifficultyModifier Difficulty);

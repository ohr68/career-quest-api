using CareerQuest.Modules.Players.Domain.Players;

namespace CareerQuest.Modules.Players.Application.Players.GetQuests;

public sealed record QuestResponse(
    Guid Id,
    string Title,
    string Description,
    int XpReward,
    DifficultyModifier Difficulty,
    DateTime ExpiresAtUtc,
    DateTime? CompletedAtUtc);

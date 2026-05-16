namespace CareerQuest.Modules.Players.Application.Players.LogXp;

public sealed record LogXpResponse(
    int BaseAmount,
    int FinalAmount,
    float Multiplier,
    int CurrentXp,
    int CurrentLevel,
    int XpToNextLevel,
    bool LeveledUp,
    int LevelsGained,
    IReadOnlyList<int> LevelUps,
    int TotalXp,
    string Action,
    DateTime OccurredAtUtc
);

namespace CareerQuest.Modules.Players.Domain.Players;

public sealed record XpResult(
    int BaseAmount,
    int FinalAmount,
    float Multiplier,
    int CurrentXp,
    int CurrentLevel,
    int XpToNextLevel,
    int LevelsGained,
    int TotalXp,
    IReadOnlyList<int> LevelUps,
    IReadOnlyList<XpTransaction> Transactions);

using CareerQuest.Common.Domain.Abstractions;

namespace CareerQuest.Modules.Players.Domain.Players;

public sealed class PlayerProgression : Entity
{
    private readonly List<XpTransaction> _transactions = [];

    private PlayerProgression()
    {
    }

    public Guid PlayerId { get; init; }

    public int CurrentLevel { get; private set; }

    public int CurrentXp { get; private set; }

    public int TotalXp { get; private set; }

    public int XpToNextLevel { get; private set; }

    public int SkillPoints { get; private set; }

    public IReadOnlyCollection<XpTransaction> Transactions => _transactions.ToList();

    public static PlayerProgression Create(Guid playerId)
    {
        return new PlayerProgression
        {
            PlayerId = playerId,
            CurrentLevel = 1,
            CurrentXp = 0,
            TotalXp = 0,
            XpToNextLevel = 100,
            SkillPoints = 0,
        };
    }

    public XpResult AddXp(
        int amount,
        string action,
        DifficultyModifier modifier,
        string? notes = null)
    {
        int multiplier = CalculateDifficultyMultiplier(modifier);
        int finalAmount = (int)MathF.Round(amount * multiplier);

        int startingLevel = CurrentLevel;
        var levelUps = new List<int>();

        TotalXp += finalAmount;
        CurrentXp += finalAmount;

        var transaction = XpTransaction.Create(
            PlayerId,
            action,
            finalAmount,
            multiplier,
            notes);

        _transactions.Add(transaction);

        ProcessLevelUps(levelUps);

        Raise(new PlayerXpEarnedDomainEvent(
            PlayerId,
            finalAmount,
            CurrentLevel,
            TotalXp));

        return new XpResult(
            amount,
            finalAmount,
            multiplier,
            CurrentXp,
            CurrentLevel,
            XpToNextLevel,
            CurrentLevel - startingLevel,
            TotalXp,
            levelUps,
            _transactions.ToList()
        );
    }

    private void ProcessLevelUps(List<int> levelUps)
    {
        while (CurrentXp >= XpToNextLevel)
        {
            CurrentXp -= XpToNextLevel;

            CurrentLevel++;

            SkillPoints++;

            XpToNextLevel = CalculateXpToNextLevel(CurrentLevel);

            levelUps.Add(CurrentLevel);

            Raise(new PlayerLeveledUpDomainEvent(
                PlayerId,
                CurrentLevel));
        }
    }

    private static int CalculateXpToNextLevel(int level)
    {
        return 100 + (level - 1) * 25;
    }

    private static int CalculateDifficultyMultiplier(DifficultyModifier difficultyModifier)
    {
        return difficultyModifier switch
        {
            DifficultyModifier.Easy => 1,
            DifficultyModifier.Medium => 2,
            DifficultyModifier.High => 3,
            _ => 1,
        };
    }
}

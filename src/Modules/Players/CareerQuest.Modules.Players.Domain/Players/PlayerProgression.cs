namespace CareerQuest.Modules.Players.Domain.Players;

public sealed class PlayerProgression
{
    private PlayerProgression()
    {
    }

    public Guid PlayerId { get; private set; }

    public int CurrentLevel { get; private set; }

    public int CurrentXp { get; private set; }

    public int TotalXp { get; private set; }

    public int XpToNextLevel { get; private set; }

    public int SkillPoints { get; private set; }

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
}

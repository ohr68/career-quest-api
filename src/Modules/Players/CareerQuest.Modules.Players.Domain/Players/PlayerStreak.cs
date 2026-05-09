namespace CareerQuest.Modules.Players.Domain.Players;

public sealed class PlayerStreak
{
    private PlayerStreak()
    {
    }

    public Guid PlayerId { get; private set; }

    public int CurrentDays { get; private set; }

    public int LongestDays { get; private set; }

    public decimal CurrentMultiplier { get; private set; }

    public DateTime LastActivityDateUtc { get; private set; }

    public static PlayerStreak Create(Guid playerId)
    {
        return new PlayerStreak
        {
            PlayerId = playerId,
            CurrentDays = 0,
            LongestDays = 0,
            CurrentMultiplier = 1,
            LastActivityDateUtc = DateTime.UtcNow,
        };
    }
}

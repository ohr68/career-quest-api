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

    public DateOnly LastActivityDate { get; private set; }

    public static PlayerStreak Create(Guid playerId)
    {
        return new PlayerStreak
        {
            PlayerId = playerId,
            CurrentDays = 0,
            LongestDays = 0,
            CurrentMultiplier = 1,
            LastActivityDate = DateOnly.MinValue,
        };
    }

    public void RegisterActivity(DateOnly activityDate)
    {
        // Already counted today - just refresh the timestamp
        if (CurrentDays > 0 && activityDate == LastActivityDate)
        {
            return;
        }

        // Consecutive day extends the streak; anything else starts a new one.
        // CurrentDays == 0 covers the first-ever activity (Create stamps 
        // LastActivityDateUtc with "today", so the date check alone isn't enough).
        CurrentDays = CurrentDays > 0 && activityDate.DayNumber == LastActivityDate.DayNumber + 1
            ? CurrentDays + 1
            : 1;

        LongestDays = Math.Max(LongestDays, CurrentDays);
        CurrentMultiplier = CalculateMultiplier(CurrentDays);
        LastActivityDate = activityDate;
    }

    private static decimal CalculateMultiplier(int streakDays)
    {
        return streakDays switch
        {
            >= 30 => 2.0m,
            >= 14 => 1.5m,
            >= 7 => 1.25m,
            >= 3 => 1.1m,
            _ => 1.0m,
        };
    }
}

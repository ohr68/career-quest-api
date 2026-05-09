namespace CareerQuest.Modules.Players.Domain.Players;

public sealed class PlayerStatistics
{
    private PlayerStatistics()
    {
    }

    public Guid PlayerId { get; private set; }

    public int TotalPostsPublished { get; private set; }

    public int TotalCommits { get; private set; }

    public int TotalApplications { get; private set; }

    public int TotalNetworkingInteractions { get; private set; }

    public int TotalQuestsCompleted { get; private set; }

    public int TotalAchievementsUnlocked { get; private set; }

    public static PlayerStatistics Create(Guid playerId)
    {
        return new PlayerStatistics
        {
            PlayerId = playerId,
        };
    }
}

namespace CareerQuest.Modules.Players.Domain.Players;

public sealed class PlayerTitle
{
    private PlayerTitle()
    {
    }

    public Guid PlayerId { get; private set; }

    public TitleType TitleType { get; private set; }

    public bool IsCurrent { get; private set; }

    public DateTime UnlockedAtUtc { get; private set; }

    public static PlayerTitle Create(
        Guid playerId,
        TitleType titleType,
        DateTime utcNow,
        bool isCurrent = false)
    {
        return new PlayerTitle
        {
            PlayerId = playerId,
            TitleType = titleType,
            IsCurrent = isCurrent,
            UnlockedAtUtc = utcNow,
        };
    }

    public void SetAsCurrent()
    {
        IsCurrent = true;
    }

    public void RemoveAsCurrent()
    {
        IsCurrent = false;
    }
}

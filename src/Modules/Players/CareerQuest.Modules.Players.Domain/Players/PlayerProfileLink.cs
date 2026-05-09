namespace CareerQuest.Modules.Players.Domain.Players;

public sealed class PlayerProfileLink
{
    public Guid PlayerId { get; private set; }

    public ProfilePlatform Platform { get; private set; }

    public string Url { get; private set; }
}

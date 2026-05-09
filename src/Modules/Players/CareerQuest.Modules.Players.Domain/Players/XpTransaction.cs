namespace CareerQuest.Modules.Players.Domain.Players;

public sealed class XpTransaction
{
    public Guid Id { get; private set; }

    public Guid PlayerId { get; private set; }

    public int Amount { get; private set; }

    public string Source { get; private set; }

    public DateTime EarnedAtUtc { get; private set; }
}

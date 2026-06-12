namespace CareerQuest.Modules.Players.Domain.Players;

public sealed class XpTransaction
{
    private XpTransaction()
    {
    }

    public Guid PlayerId { get; init; }

    public string Action { get; init; } = null!;

    public int Amount { get; init; }

    public float Multiplier { get; init; }

    public DateTime EarnedAtUtc { get; init; }

    public string? Notes { get; init; }

    internal static XpTransaction Create(
        Guid playerId,
        string action,
        int amount,
        float multiplier,
        string? notes,
        DateTime utcNow) =>
        new()
        {
            PlayerId = playerId,
            Action = action,
            Amount = amount,
            Multiplier = multiplier,
            EarnedAtUtc = utcNow,
            Notes = notes,
        };
}
namespace CareerQuest.Modules.Players.Application.Players.SearchXpHistory;

public sealed record SearchXpHistoryResponse(
    int Page,
    int PageSize,
    int TotalCount,
    IReadOnlyCollection<XpResponse> Items);

public sealed class XpResponse
{
    public int Id { get; init; }
    public string Action { get; init; } = default!;
    public int Amount { get; init; }
    public int Multiplier { get; init; }
    public DateTime EarnedAtUtc { get; init; }
    public string? Notes { get; init; }
}

using CareerQuest.Common.Application.Messaging;

namespace CareerQuest.Modules.Players.Application.Players.SearchXpHistory;

public sealed record SearchXpHistoryQuery(
    Guid PlayerId,
    int Page,
    int PageSize) : IQuery<SearchXpHistoryResponse>;

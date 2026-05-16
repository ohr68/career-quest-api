using System.Data.Common;
using CareerQuest.Common.Application.Data;
using CareerQuest.Common.Application.Messaging;
using CareerQuest.Common.Domain.Abstractions;
using Dapper;

namespace CareerQuest.Modules.Players.Application.Players.SearchXpHistory;

internal sealed class SearchXpHistoryQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<SearchXpHistoryQuery, SearchXpHistoryResponse>
{
    public async Task<Result<SearchXpHistoryResponse>> Handle(SearchXpHistoryQuery request,
        CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        var parameters = new SearchXpParameters(
            request.PlayerId,
            request.PageSize,
            (request.Page - 1) * request.PageSize);

        IReadOnlyCollection<XpResponse> xpHistory = await GetXpHistoryAsync(connection, parameters);

        int totalCount = await CountEventsAsync(connection, parameters);

        return new SearchXpHistoryResponse(request.Page, request.PageSize, totalCount, xpHistory);
    }

    private static async Task<IReadOnlyCollection<XpResponse>> GetXpHistoryAsync(
        DbConnection connection,
        SearchXpParameters parameters)
    {
        const string sql =
            $"""
             SELECT 
                id AS {nameof(XpResponse.Id)},
                action AS {nameof(XpResponse.Action)},
                amount AS {nameof(XpResponse.Amount)},
                multiplier AS {nameof(XpResponse.Multiplier)},
                earned_at_utc AS {nameof(XpResponse.EarnedAtUtc)},
                notes AS {nameof(XpResponse.Notes)}
             FROM players.player_xp_transactions
             WHERE 
                 player_id = @PlayerId
             ORDER BY earned_at_utc DESC
             OFFSET @skip
             LIMIT @take
             """;

        List<XpResponse> xpEarned = (await connection.QueryAsync<XpResponse>(sql, parameters)).AsList();

        return xpEarned;
    }

    private static async Task<int> CountEventsAsync(DbConnection connection, SearchXpParameters parameters)
    {
        const string sql =
            """
            SELECT COUNT(*)
            FROM players.player_xp_transactions
            WHERE
               player_id = @PlayerId
            """;

        int totalCount = await connection.ExecuteScalarAsync<int>(sql, parameters);

        return totalCount;
    }

    private sealed record SearchXpParameters(
        Guid PlayerId,
        int Take,
        int Skip);
}

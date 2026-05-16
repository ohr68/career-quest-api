using CareerQuest.Common.Domain.Abstractions;
using CareerQuest.Common.Presentation.ApiResults;
using CareerQuest.Common.Presentation.Endpoints;
using CareerQuest.Modules.Players.Application.Abstractions.Authentication;
using CareerQuest.Modules.Players.Application.Players.SearchXpHistory;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace CareerQuest.Modules.Players.Presentation.Players;

internal sealed class SearchXpHistory : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("xp/history", async (
                ISender sender,
                IPlayerContext playerContext,
                int page = 0,
                int pageSize = 15) =>
            {
                Result<SearchXpHistoryResponse> result = await sender.Send(
                    new SearchXpHistoryQuery(playerContext.PlayerId, page, pageSize));

                return result.Match(Results.Ok, ApiResults.Problem);
            })
            .RequireAuthorization(Permissions.ReadProgression)
            .WithTags(Tags.Xp);
    }
}

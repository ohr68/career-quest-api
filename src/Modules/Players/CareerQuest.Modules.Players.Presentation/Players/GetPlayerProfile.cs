using CareerQuest.Common.Domain.Abstractions;
using CareerQuest.Common.Presentation.ApiResults;
using CareerQuest.Common.Presentation.Endpoints;
using CareerQuest.Modules.Players.Application.Abstractions.Authentication;
using CareerQuest.Modules.Players.Application.Players.GetPlayer;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace CareerQuest.Modules.Players.Presentation.Players;

internal sealed class GetPlayerProfile : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(
                "players/me",
                async (
                    IPlayerContext playerContext,
                    ISender sender) =>
                {
                    Result<PlayerResponse> result = await sender.Send(
                        new GetPlayerQuery(
                            playerContext.PlayerId)
                    );

                    return result.Match(Results.Ok, ApiResults.Problem);
                })
            .RequireAuthorization(Permissions.GetProfile)
            .WithTags(Tags.Players);
    }
}

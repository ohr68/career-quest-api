using CareerQuest.Common.Domain.Abstractions;
using CareerQuest.Common.Presentation.ApiResults;
using CareerQuest.Common.Presentation.Endpoints;
using CareerQuest.Modules.Players.Application.Abstractions.Authentication;
using CareerQuest.Modules.Players.Application.Players.GetCareerCoaching;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace CareerQuest.Modules.Players.Presentation.Players;

internal sealed class GetCareerCoaching : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(
                "players/me/coaching",
                async (
                    IPlayerContext playerContext,
                    ISender sender) =>
                {
                    Result<CoachingResponse> result = await sender.Send(
                        new GetCareerCoachingQuery(playerContext.PlayerId));

                    return result.Match(Results.Ok, ApiResults.Problem);
                })
            .RequireAuthorization(Permissions.GetProfile)
            .WithTags(Tags.Players);
    }
}

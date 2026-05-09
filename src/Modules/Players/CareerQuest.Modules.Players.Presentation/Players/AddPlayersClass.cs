using CareerQuest.Common.Domain.Abstractions;
using CareerQuest.Common.Presentation.ApiResults;
using CareerQuest.Common.Presentation.Endpoints;
using CareerQuest.Modules.Players.Application.Abstractions.Authentication;
using CareerQuest.Modules.Players.Application.Players.AddPlayerClass;
using CareerQuest.Modules.Players.Domain.Players;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace CareerQuest.Modules.Players.Presentation.Players;

internal sealed class AddPlayersClass : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("players/classes", async (
                IPlayerContext playerContext,
                Request request,
                ISender sender) =>
            {
                Result result = await sender.Send(
                    new AddPlayerClassCommand(
                        playerContext.PlayerId,
                        request.ClassType));

                return result.Match(Results.NoContent, ApiResults.Problem);
            })
            .RequireAuthorization()
            .WithTags(Tags.Players);
    }

    internal sealed class Request
    {
        public PlayerClassType ClassType { get; init; }
    }
}

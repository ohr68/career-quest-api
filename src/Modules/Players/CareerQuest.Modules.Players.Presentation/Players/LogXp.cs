using CareerQuest.Common.Domain.Abstractions;
using CareerQuest.Common.Presentation.ApiResults;
using CareerQuest.Common.Presentation.Endpoints;
using CareerQuest.Modules.Players.Application.Abstractions.Authentication;
using CareerQuest.Modules.Players.Application.Players.LogXp;
using CareerQuest.Modules.Players.Domain.Players;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace CareerQuest.Modules.Players.Presentation.Players;

internal sealed class LogXp : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(
                "xp/log",
                async (
                    IPlayerContext playerContext,
                    Request request,
                    ISender sender) =>
                {
                    Result<LogXpResponse> result = await sender.Send(
                        new LogXpCommand(
                            playerContext.PlayerId,
                            request.Action,
                            request.Amount,
                            request.Modifier,
                            request.Notes));

                    return result.Match(Results.Ok, ApiResults.Problem);
                })
            .RequireAuthorization(Permissions.EarnXp)
            .WithTags(Tags.Xp);
    }

    internal sealed class Request
    {
        public string Action { get; init; }
        public int Amount { get; init; }
        public DifficultyModifier Modifier { get; init; }
        public string? Notes { get; init; }
    }
}

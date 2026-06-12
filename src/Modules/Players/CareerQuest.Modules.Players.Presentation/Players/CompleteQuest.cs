using CareerQuest.Common.Domain.Abstractions;
using CareerQuest.Common.Presentation.ApiResults;
using CareerQuest.Common.Presentation.Endpoints;
using CareerQuest.Modules.Players.Application.Abstractions.Authentication;
using CareerQuest.Modules.Players.Application.Players.CompleteQuest;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace CareerQuest.Modules.Players.Presentation.Players;

internal sealed class CompleteQuest : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(
                "quests/{questId:guid}/complete",
                async (
                    Guid questId,
                    IPlayerContext playerContext,
                    ISender sender) =>
                {
                    Result result = await sender.Send(
                        new CompleteQuestCommand(playerContext.PlayerId, questId));

                    return result.Match(Results.NoContent, ApiResults.Problem);
                })
            .RequireAuthorization(Permissions.CompleteQuest)
            .WithTags(Tags.Quests);
    }
}

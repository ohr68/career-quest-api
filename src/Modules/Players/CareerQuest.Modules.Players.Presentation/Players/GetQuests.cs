using CareerQuest.Common.Domain.Abstractions;
using CareerQuest.Common.Presentation.ApiResults;
using CareerQuest.Common.Presentation.Endpoints;
using CareerQuest.Modules.Players.Application.Abstractions.Authentication;
using CareerQuest.Modules.Players.Application.Players.GetQuests;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace CareerQuest.Modules.Players.Presentation.Players;

internal sealed class GetQuests : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(
                "quests",
                async (
                    IPlayerContext playerContext,
                    ISender sender) =>
                {
                    Result<IReadOnlyCollection<QuestResponse>> result = await sender.Send(
                        new GetQuestsQuery(playerContext.PlayerId));

                    return result.Match(Results.Ok, ApiResults.Problem);
                })
            .RequireAuthorization(Permissions.ReadQuests)
            .WithTags(Tags.Quests);
    }
}

using CareerQuest.Common.Domain.Abstractions;
using CareerQuest.Common.Presentation.ApiResults;
using CareerQuest.Common.Presentation.Endpoints;
using CareerQuest.Modules.Players.Application.Abstractions.Intelligence;
using CareerQuest.Modules.Players.Application.Players.ClassifyActivity;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace CareerQuest.Modules.Players.Presentation.Players;

internal sealed class ClassifyActivity : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(
                "xp/classify",
                async (
                    Request request,
                    ISender sender) =>
                {
                    Result<ActivityClassification> result = await sender.Send(
                        new ClassifyActivityQuery(request.Description));

                    return result.Match(Results.Ok, ApiResults.Problem);
                })
            .RequireAuthorization(Permissions.EarnXp)
            .WithTags(Tags.Xp);
    }

    internal sealed class Request
    {
        public string Description { get; init; } = null!;
    }
}

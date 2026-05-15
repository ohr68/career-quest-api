using CareerQuest.Common.Domain.Abstractions;
using CareerQuest.Common.Presentation.ApiResults;
using CareerQuest.Common.Presentation.Endpoints;
using CareerQuest.Modules.Players.Application.Abstractions.Authentication;
using CareerQuest.Modules.Players.Application.Players.CompleteProfile;
using CareerQuest.Modules.Players.Domain.Players;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace CareerQuest.Modules.Players.Presentation.Players;

internal sealed class CompleteProfile : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut(
                "players/me/complete-profile",
                async (
                    IPlayerContext playerContext,
                    Request request,
                    ISender sender) =>
                {
                    Result result = await sender.Send(
                        new CompleteProfileCommand(
                            playerContext.PlayerId,
                            request.Headline,
                            request.AvatarUrl,
                            request.CareerStage,
                            request.Classes,
                            request.Specializations));

                    return result.Match(
                        Results.NoContent,
                        ApiResults.Problem);
                })
            .RequireAuthorization(Permissions.CompleteProfile)
            .WithTags(Tags.Players);
    }

    internal sealed class Request
    {
        public string? Headline { get; init; }
        public Uri? AvatarUrl { get; init; }
        public CareerStage CareerStage { get; init; }
        public IReadOnlyCollection<PlayerClassType> Classes { get; init; }
        public IReadOnlyCollection<PlayerSpecializationType> Specializations { get; init; }
    }
}

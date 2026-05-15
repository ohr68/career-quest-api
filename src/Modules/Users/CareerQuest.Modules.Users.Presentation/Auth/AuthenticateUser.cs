using CareerQuest.Common.Domain.Abstractions;
using CareerQuest.Common.Presentation.ApiResults;
using CareerQuest.Common.Presentation.Endpoints;
using CareerQuest.Modules.Users.Application.Abstractions.Identity;
using CareerQuest.Modules.Users.Application.Auth.Login;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace CareerQuest.Modules.Users.Presentation.Auth;

internal sealed class AuthenticateUser : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("auth/login", async (Request request, ISender sender) =>
            {
                Result<AuthResponse> result = await sender.Send(new LoginCommand(
                    request.Email,
                    request.Password));

                return result.Match(Results.Ok, ApiResults.Problem);
            })
            .AllowAnonymous()
            .WithTags(Tags.Auth);
    }

    internal sealed class Request
    {
        public string Email { get; init; }

        public string Password { get; init; }
    }
}

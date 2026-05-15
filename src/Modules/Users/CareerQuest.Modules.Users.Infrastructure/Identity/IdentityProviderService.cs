using System.Net;
using CareerQuest.Common.Domain.Abstractions;
using CareerQuest.Modules.Users.Application.Abstractions.Identity;
using Microsoft.Extensions.Logging;

namespace CareerQuest.Modules.Users.Infrastructure.Identity;

internal sealed class IdentityProviderService(
    KeyCloakAdminClient keyCloakAdminClient,
    KeyCloakClient keyCloakClient,
    ILogger<IdentityProviderService> logger) : IIdentityProviderService
{
    private const string PasswordCredentialType = "Password";

    public async Task<Result<AuthResponse>> AuthenticateAsync(LoginModel login,
        CancellationToken cancellationToken = default)
    {
        var authRequest = new AuthRequest(login.Email, login.Password);

        try
        {
            TokenResponse authResult = await keyCloakClient.AuthenticateUserAsync(authRequest, cancellationToken);

            return new AuthResponse(
                authResult.AccessToken,
                authResult.ExpiresIn,
                authResult.RefreshToken,
                authResult.RefreshTokenExpiresIn
            );
        }
        catch (HttpRequestException e) when (e.StatusCode == HttpStatusCode.NotFound)
        {
            logger.LogError(e, "User authentication failed.");

            return Result.Failure<AuthResponse>(IdentityProviderErrors.EmailNotFound);
        }
        catch (HttpRequestException e)
        {
            logger.LogError(e, "User authentication failed.");

            return Result.Failure<AuthResponse>(IdentityProviderErrors.EmailNotFound);
        }
    }

    public async Task<Result<AuthResponse>> RefreshTokenAsync(string refreshToken,
        CancellationToken cancellationToken = default)
    {
        var refreshTokenRequest = new RefreshTokenRequest(refreshToken);

        try
        {
            TokenResponse authResult = await keyCloakClient.RefreshTokenAsync(refreshTokenRequest, cancellationToken);

            return new AuthResponse(
                authResult.AccessToken,
                authResult.ExpiresIn,
                authResult.RefreshToken,
                authResult.RefreshTokenExpiresIn
            );
        }
        catch (HttpRequestException e) when (e.StatusCode == HttpStatusCode.Unauthorized)
        {
            logger.LogError(e, "User authentication failed.");

            return Result.Failure<AuthResponse>(IdentityProviderErrors.InvalidRefreshToken);
        }
    }

    public async Task<Result<string>> RegisterUserAsync(UserModel user, CancellationToken cancellationToken = default)
    {
        var userRepresentation = new UserRepresentation(
            user.Email,
            user.Email,
            user.FirstName,
            user.LastName,
            true,
            true,
            [
                new CredentialRepresentation(
                    PasswordCredentialType,
                    user.Password,
                    false),
            ]);

        try
        {
            string identityId = await keyCloakAdminClient.RegisterUserAsync(userRepresentation, cancellationToken);

            return identityId;
        }
        catch (HttpRequestException e) when (e.StatusCode == HttpStatusCode.Conflict)
        {
            logger.LogError(e, "User registration failed.");

            return Result.Failure<string>(IdentityProviderErrors.EmailIsNotUnique);
        }
    }
}

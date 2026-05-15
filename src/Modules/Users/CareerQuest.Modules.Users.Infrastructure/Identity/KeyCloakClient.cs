using System.Net.Http.Json;
using Microsoft.Extensions.Options;

namespace CareerQuest.Modules.Users.Infrastructure.Identity;

internal sealed class KeyCloakClient(HttpClient httpClient, IOptions<KeyCloakOptions> options)
{
    private const string PasswordGrantType = "password";
    private readonly KeyCloakOptions _options = options.Value;


    internal async Task<TokenResponse> AuthenticateUserAsync(AuthRequest authRequest,
        CancellationToken cancellationToken = default)
    {
        var authRequestParameters = new KeyValuePair<string, string>[]
        {
            new("client_id", _options.ConfidentialClientId),
            new("client_secret", _options.ConfidentialClientSecret),
            new("grant_type", PasswordGrantType),
            new("username", authRequest.Username),
            new("password", authRequest.Password),
        };

        using var authRequestContent = new FormUrlEncodedContent(authRequestParameters);

        HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(
            string.Empty,
            authRequestContent,
            cancellationToken);

        httpResponseMessage.EnsureSuccessStatusCode();

        return await httpResponseMessage.Content.ReadFromJsonAsync<TokenResponse>(cancellationToken);
    }

    internal async Task<TokenResponse> RefreshTokenAsync(RefreshTokenRequest refreshTokenRequest,
        CancellationToken cancellationToken = default)
    {
        var refreshTokenRequestParameters = new KeyValuePair<string, string>[]
        {
            new("client_id", _options.ConfidentialClientId),
            new("client_secret", _options.ConfidentialClientSecret),
            new("grant_type", PasswordGrantType),
            new("refresh_token", refreshTokenRequest.RefreshToken),
        };

        using var refreshTokenRequestContent = new FormUrlEncodedContent(refreshTokenRequestParameters);

        HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(
            string.Empty,
            refreshTokenRequestContent,
            cancellationToken);

        httpResponseMessage.EnsureSuccessStatusCode();

        return await httpResponseMessage.Content.ReadFromJsonAsync<TokenResponse>(cancellationToken);
    }
}

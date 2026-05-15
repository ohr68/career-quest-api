using System.Text.Json.Serialization;

namespace CareerQuest.Modules.Users.Infrastructure.Identity;

internal sealed class TokenResponse
{
    [JsonPropertyName("access_token")] public string AccessToken { get; init; }

    [JsonPropertyName("expires_in")] public int ExpiresIn { get; init; }

    [JsonPropertyName("refresh_expires_in")]
    public int RefreshTokenExpiresIn { get; init; }

    [JsonPropertyName("refresh_token")] public string RefreshToken { get; init; }

    [JsonPropertyName("token_type")] public string? TokenType { get; init; }

    [JsonPropertyName("not-before-policy")]
    public int NotValidBeforePolicy { get; init; }

    [JsonPropertyName("scope")] public string? Scope { get; init; }

    [JsonPropertyName("session_state")] public string? SessionState { get; init; }
}

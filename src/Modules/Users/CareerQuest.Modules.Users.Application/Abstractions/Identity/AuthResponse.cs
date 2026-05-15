namespace CareerQuest.Modules.Users.Application.Abstractions.Identity;

public sealed record AuthResponse(
    string AccessToken,
    int ExpiresIn,
    string RefreshToken,
    int RefreshTokenExpiresIn);

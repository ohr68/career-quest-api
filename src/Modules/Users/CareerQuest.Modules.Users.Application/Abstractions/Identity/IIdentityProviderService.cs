using CareerQuest.Common.Domain.Abstractions;

namespace CareerQuest.Modules.Users.Application.Abstractions.Identity;

public interface IIdentityProviderService
{
    Task<Result<AuthResponse>> AuthenticateAsync(LoginModel login, CancellationToken cancellationToken = default);
    Task<Result<AuthResponse>> RefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default);
    Task<Result<string>> RegisterUserAsync(UserModel user, CancellationToken cancellationToken = default);
}

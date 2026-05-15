using CareerQuest.Common.Application.Messaging;
using CareerQuest.Common.Domain.Abstractions;
using CareerQuest.Modules.Users.Application.Abstractions.Identity;
using CareerQuest.Modules.Users.Domain.Users;

namespace CareerQuest.Modules.Users.Application.Auth.Login;

internal sealed class LoginCommandHandler(
    IUserRepository userRepository,
    IIdentityProviderService identityProviderService)
    : ICommandHandler<LoginCommand, AuthResponse>
{
    public async Task<Result<AuthResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        User? user = await userRepository.GetByEmailAsNoTrackingAsync(request.Email, cancellationToken);

        if (user is null)
        {
            return Result.Failure<AuthResponse>(UserErrors.NotFound(request.Email));
        }

        Result<AuthResponse> result =
            await identityProviderService.AuthenticateAsync(
                new LoginModel(
                    request.Email,
                    request.Password
                ),
                cancellationToken);

        return result.IsFailure ? Result.Failure<AuthResponse>(result.Error) : result;
    }
}

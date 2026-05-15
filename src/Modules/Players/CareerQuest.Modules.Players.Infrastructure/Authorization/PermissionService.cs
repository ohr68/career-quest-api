using CareerQuest.Common.Application.Authorization;
using CareerQuest.Common.Domain.Abstractions;

namespace CareerQuest.Modules.Players.Infrastructure.Authorization;

internal sealed class PermissionService(
    IUserAuthorizationProvider userAuthorizationProvider
) : IPermissionService
{
    public async Task<Result<PermissionsResponse>> GetUserPermissionsAsync(string identityId)
    {
        if (!Guid.TryParse(identityId, out Guid userId))
        {
            return Result.Failure<PermissionsResponse>(AuthorizationProviderErrors.UnidentifiedUser);
        }

        UserAuthorizationSnapshot authorizationSnapshot =
            await userAuthorizationProvider.GetAsync(userId);

        return new PermissionsResponse(authorizationSnapshot.UserId, authorizationSnapshot.Permissions);
    }
}

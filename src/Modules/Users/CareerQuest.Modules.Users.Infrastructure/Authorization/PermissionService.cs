using CareerQuest.Common.Application.Authorization;
using CareerQuest.Common.Domain.Abstractions;
using CareerQuest.Modules.Users.Application.Users.GetUserPermissions;
using MediatR;

namespace CareerQuest.Modules.Users.Infrastructure.Authorization;

internal sealed class PermissionService(ISender sender) : IPermissionService
{
    public async Task<Result<PermissionsResponse>> GetUserPermissionsAsync(string identityId)
    {
        return await sender.Send(new GetUserPermissionsQuery(identityId));
    }
}

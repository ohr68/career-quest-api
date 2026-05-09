using CareerQuest.Common.Domain.Abstractions;

namespace CareerQuest.Common.Application.Authorization;

public interface IPermissionService
{
    Task<Result<PermissionsResponse>> GetUserPermissionsAsync(string identityId);
}

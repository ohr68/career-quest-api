using CareerQuest.Common.Application.Authorization;
using CareerQuest.Common.Application.Messaging;

namespace CareerQuest.Modules.Users.Application.Users.GetUserPermissions;

public sealed record GetUserPermissionsQuery(string IdentityId) : IQuery<PermissionsResponse>;

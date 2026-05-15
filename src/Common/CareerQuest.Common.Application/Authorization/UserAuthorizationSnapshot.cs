namespace CareerQuest.Common.Application.Authorization;

public sealed record UserAuthorizationSnapshot(
    Guid UserId,
    string Email,
    HashSet<string> Roles,
    HashSet<string> Permissions);

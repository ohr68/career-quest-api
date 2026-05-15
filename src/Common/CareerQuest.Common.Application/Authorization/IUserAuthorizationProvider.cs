namespace CareerQuest.Common.Application.Authorization;

public interface IUserAuthorizationProvider
{
    Task<UserAuthorizationSnapshot> GetAsync(
        Guid userId,
        CancellationToken cancellationToken = default);
}

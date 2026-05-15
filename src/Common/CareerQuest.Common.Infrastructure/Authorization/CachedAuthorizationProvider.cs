using CareerQuest.Common.Application.Authorization;
using CareerQuest.Common.Application.Caching;

namespace CareerQuest.Common.Infrastructure.Authorization;

public sealed class CachedAuthorizationProvider(
    IUserAuthorizationProvider inner,
    ICacheService cache)
    : IUserAuthorizationProvider
{
    private const string Prefix = "auth";

    public async Task<UserAuthorizationSnapshot> GetAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        string key = $"{Prefix}:{userId}";

        UserAuthorizationSnapshot? cached =
            await cache.GetAsync<UserAuthorizationSnapshot>(key, cancellationToken);

        if (cached is not null)
        {
            return cached;
        }

        UserAuthorizationSnapshot result =
            await inner.GetAsync(userId, cancellationToken);

        await cache.SetAsync(
            key,
            result,
            TimeSpan.FromMinutes(30),
            cancellationToken);

        return result;
    }
}

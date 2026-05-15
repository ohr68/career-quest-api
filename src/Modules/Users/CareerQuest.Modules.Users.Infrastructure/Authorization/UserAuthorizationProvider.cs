using System.Data.Common;
using CareerQuest.Common.Application.Authorization;
using CareerQuest.Common.Application.Data;
using Dapper;

namespace CareerQuest.Modules.Users.Infrastructure.Authorization;

public sealed class UserAuthorizationProvider(
    IDbConnectionFactory dbConnectionFactory)
    : IUserAuthorizationProvider
{
    public async Task<UserAuthorizationSnapshot> GetAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql = """
                           SELECT
                               u.id AS UserId,
                               u.email AS Email,
                               r.name AS Role,
                               p.code AS Permission
                           FROM users.users u
                           LEFT JOIN users.user_roles ur ON ur.user_id = u.id
                           LEFT JOIN users.roles r ON r.name = ur.roles_name
                           LEFT JOIN users.role_permissions rp ON rp.role_name = r.name
                           LEFT JOIN users.permissions p ON p.code = rp.permission_code
                           WHERE u.id = @UserId
                           """;

        var lookup = new Dictionary<Guid, UserAuthorizationSnapshot>();

        await connection.QueryAsync<UserRow, string?, string?, UserAuthorizationSnapshot>(
            sql,
            (user, role, permission) =>
            {
                if (!lookup.TryGetValue(user.UserId, out UserAuthorizationSnapshot? snapshot))
                {
                    snapshot = new UserAuthorizationSnapshot(
                        user.UserId,
                        user.Email,
                        new HashSet<string>(),
                        new HashSet<string>());

                    lookup[user.UserId] = snapshot;
                }

                if (role is not null)
                {
                    snapshot.Roles.Add(role);
                }

                if (permission is not null)
                {
                    snapshot.Permissions.Add(permission);
                }

                return snapshot;
            },
            new { UserId = userId },
            splitOn: "Role,Permission");

        return lookup.Values.Single();
    }

    private sealed record UserRow(Guid UserId, string Email);
}

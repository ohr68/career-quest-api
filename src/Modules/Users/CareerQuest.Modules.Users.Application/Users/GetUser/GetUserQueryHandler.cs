using System.Data.Common;
using CareerQuest.Common.Application.Data;
using CareerQuest.Common.Application.Messaging;
using CareerQuest.Common.Domain.Abstractions;
using CareerQuest.Modules.Users.Domain.Users;
using Dapper;

namespace CareerQuest.Modules.Users.Application.Users.GetUser;

internal sealed class GetUserQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetUserQuery, UserResponse>
{
    public async Task<Result<UserResponse>> Handle(
        GetUserQuery request,
        CancellationToken cancellationToken)
    {
        await using DbConnection connection =
            await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            """
            SELECT
                u.id AS Id,
                u.email AS Email,
                u.first_name AS FirstName,
                u.last_name AS LastName,
                r.name AS Role,
                p.code AS Permission
            FROM users.users u
            LEFT JOIN users.user_roles ur
                ON ur.user_id = u.id
            LEFT JOIN users.roles r
                ON r.name = ur.roles_name
            LEFT JOIN users.role_permissions rp
                ON rp.role_name = r.name
            LEFT JOIN users.permissions p
                ON p.code = rp.permission_code
            WHERE u.id = @UserId
            """;

        Dictionary<Guid, UserResponseData> users = [];

        await connection.QueryAsync<UserResponseData, string?, string?, UserResponseData>(
            sql,
            (user, role, permission) =>
            {
                if (!users.TryGetValue(user.Id, out UserResponseData? existingUser))
                {
                    existingUser = user;

                    users.Add(existingUser.Id, existingUser);
                }

                if (!string.IsNullOrWhiteSpace(role))
                {
                    existingUser.Roles.Add(role);
                }

                if (!string.IsNullOrWhiteSpace(permission))
                {
                    existingUser.Permissions.Add(permission);
                }

                return existingUser;
            },
            request,
            splitOn: "Role,Permission");

        UserResponseData? result =
            users.Values.SingleOrDefault();

        if (result is null)
        {
            return Result.Failure<UserResponse>(
                UserErrors.NotFound(request.UserId));
        }

        return new UserResponse
        {
            Id = result.Id,
            Email = result.Email,
            FirstName = result.FirstName,
            LastName = result.LastName,
            Roles = result.Roles
                .Distinct()
                .ToList(),
            Permissions = result.Permissions
                .Distinct()
                .ToList(),
        };
    }

    private sealed class UserResponseData
    {
        public Guid Id { get; init; } = Guid.Empty;

        public string Email { get; init; } = null!;

        public string FirstName { get; init; } = null!;

        public string LastName { get; init; } = null!;

        public HashSet<string> Roles { get; init; } = [];

        public HashSet<string> Permissions { get; init; } = [];
    }
}

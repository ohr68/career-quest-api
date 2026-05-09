using CareerQuest.Common.Application.Messaging;

namespace CareerQuest.Modules.Users.Application.Users.GetUser;

public sealed record GetUserQuery(Guid UserId) : IQuery<UserResponse>;

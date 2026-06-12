namespace CareerQuest.Modules.Users.Application.Users.GetUser;

public sealed record UserResponse
{
    public Guid Id { get; init; }

    public string Email { get; init; } = null!;

    public string FirstName { get; init; } = null!;

    public string LastName { get; init; } = null!;

    public IReadOnlyCollection<string> Roles { get; init; } = [];

    public IReadOnlyCollection<string> Permissions { get; init; } = [];
}
namespace CareerQuest.Modules.Users.Domain.Users;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<User?> GetByEmailAsNoTrackingAsync(string email, CancellationToken cancellationToken = default);

    void Insert(User user);
}

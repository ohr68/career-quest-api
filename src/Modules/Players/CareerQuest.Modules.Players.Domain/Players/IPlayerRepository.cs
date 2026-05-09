namespace CareerQuest.Modules.Players.Domain.Players;

public interface IPlayerRepository
{
    Task<Player?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    void Insert(Player user);
}

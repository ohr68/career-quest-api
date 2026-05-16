using CareerQuest.Modules.Players.Domain.Players;
using CareerQuest.Modules.Players.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace CareerQuest.Modules.Players.Infrastructure.Players;

internal sealed class PlayerRepository(PlayersDbContext context) : IPlayerRepository
{
    public async Task<Player?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Players.SingleOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    public async Task<Player?> GetCurrentProgressAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Players
            .Include(p => p.Progression)
            .SingleOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    public void Insert(Player user)
    {
        context.Players.Add(user);
    }
}

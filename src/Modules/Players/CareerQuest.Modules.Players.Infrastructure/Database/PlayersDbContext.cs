using CareerQuest.Common.Infrastructure.Inbox;
using CareerQuest.Common.Infrastructure.Outbox;
using CareerQuest.Modules.Players.Application.Abstractions.Data;
using CareerQuest.Modules.Players.Domain.Players;
using CareerQuest.Modules.Players.Infrastructure.Players;
using Microsoft.EntityFrameworkCore;

namespace CareerQuest.Modules.Players.Infrastructure.Database;

public sealed class PlayersDbContext(DbContextOptions<PlayersDbContext> options) : DbContext(options), IUnitOfWork
{
    internal DbSet<Player> Players { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Players);

        modelBuilder.ApplyConfiguration(new OutboxMessageConfiguration());
        modelBuilder.ApplyConfiguration(new OutboxMessageConsumerConfiguration());
        modelBuilder.ApplyConfiguration(new InboxMessageConfiguration());
        modelBuilder.ApplyConfiguration(new InboxMessageConsumerConfiguration());
        modelBuilder.ApplyConfiguration(new PlayerConfiguration());
    }
}

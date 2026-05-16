using CareerQuest.Modules.Players.Domain.Players;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CareerQuest.Modules.Players.Infrastructure.Players;

internal sealed class PlayerProgressionConfiguration : IEntityTypeConfiguration<PlayerProgression>
{
    public void Configure(EntityTypeBuilder<PlayerProgression> builder)
    {
        builder.ToTable("player_progressions");

        builder.HasKey(pp => pp.PlayerId);

        builder.Property(pp => pp.CurrentLevel)
            .IsRequired();

        builder.Property(pp => pp.CurrentXp)
            .IsRequired();

        builder.Property(pp => pp.TotalXp)
            .IsRequired();

        builder.Property(pp => pp.XpToNextLevel)
            .IsRequired();

        builder.Property(pp => pp.SkillPoints)
            .IsRequired();

        builder.HasIndex(pp => pp.CurrentLevel);

        builder.HasIndex(pp => pp.TotalXp);

        builder.OwnsMany(pp => pp.Transactions, t =>
        {
            t.ToTable("player_xp_transactions");

            t.WithOwner()
                .HasForeignKey(x => x.PlayerId);

            t.Property(x => x.Action).IsRequired();
        });
    }
}

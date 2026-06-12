using CareerQuest.Modules.Players.Domain.Players;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CareerQuest.Modules.Players.Infrastructure.Players;

internal sealed class PlayerQuestConfiguration : IEntityTypeConfiguration<PlayerQuest>
{
    public void Configure(EntityTypeBuilder<PlayerQuest> builder)
    {
        builder.ToTable("player_quests");

        builder.HasKey(q => q.Id);

        builder.Property(q => q.Title)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(q => q.Description)
            .HasMaxLength(2000)
            .IsRequired();

        builder.Property(q => q.XpReward)
            .IsRequired();

        builder.Property(q => q.Difficulty)
            .HasConversion<string>()
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(q => q.ExpiresAtUtc)
            .IsRequired();

        builder.HasIndex(q => q.PlayerId);

        builder.HasIndex(q => q.ExpiresAtUtc);
    }
}

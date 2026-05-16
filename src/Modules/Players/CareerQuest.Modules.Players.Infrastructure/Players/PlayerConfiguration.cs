using CareerQuest.Modules.Players.Domain.Players;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CareerQuest.Modules.Players.Infrastructure.Players;

internal sealed class PlayerConfiguration : IEntityTypeConfiguration<Player>
{
    public void Configure(EntityTypeBuilder<Player> builder)
    {
        builder.ToTable("players");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.DisplayName)
            .HasMaxLength(400)
            .IsRequired();

        builder.Property(p => p.Email)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(p => p.Headline)
            .HasMaxLength(500);

        builder.Property(p => p.CareerStage)
            .HasConversion<string>()
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(p => p.JoinedAtUtc)
            .IsRequired();

        builder.Property(p => p.LastActiveAtUtc)
            .IsRequired();

        builder.Property(p => p.AvatarUrl)
            .HasConversion(
                x => x == null ? null : x.ToString(),
                x => string.IsNullOrWhiteSpace(x)
                    ? null
                    : new Uri(x))
            .HasMaxLength(2048);

        builder.HasIndex(p => p.Email)
            .IsUnique();

        builder.HasIndex(p => p.DisplayName);

        builder.HasIndex(p => p.CareerStage);

        builder.Ignore(p => p.CurrentTitle);

        builder.HasOne(p => p.Progression)
            .WithOne()
            .HasForeignKey<PlayerProgression>(p => p.PlayerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.OwnsOne(p => p.Statistics, statistics =>
        {
            statistics.ToTable("player_statistics");

            statistics.WithOwner()
                .HasForeignKey(p => p.PlayerId);

            statistics.HasKey(p => p.PlayerId);

            statistics.Property(p => p.TotalPostsPublished)
                .IsRequired();

            statistics.Property(p => p.TotalCommits)
                .IsRequired();

            statistics.Property(p => p.TotalApplications)
                .IsRequired();

            statistics.Property(p => p.TotalNetworkingInteractions)
                .IsRequired();

            statistics.Property(p => p.TotalQuestsCompleted)
                .IsRequired();

            statistics.Property(p => p.TotalAchievementsUnlocked)
                .IsRequired();
        });

        builder.OwnsOne(p => p.Streak, streak =>
        {
            streak.ToTable("player_streaks");

            streak.WithOwner()
                .HasForeignKey(p => p.PlayerId);

            streak.HasKey(p => p.PlayerId);

            streak.Property(p => p.CurrentDays)
                .IsRequired();

            streak.Property(p => p.LongestDays)
                .IsRequired();

            streak.Property(p => p.CurrentMultiplier)
                .HasPrecision(5, 2);

            streak.Property(p => p.LastActivityDateUtc)
                .IsRequired();
        });

        builder.OwnsMany(p => p.Classes, classes =>
        {
            classes.ToTable("player_classes");

            classes.WithOwner()
                .HasForeignKey(p => p.PlayerId);

            classes.HasKey(x => new
            {
                x.PlayerId,
                x.ClassType,
            });

            classes.Property(p => p.ClassType)
                .HasConversion<int>()
                .IsRequired();

            classes.HasIndex(p => p.ClassType);
        });

        builder.OwnsMany(p => p.Specializations, specializations =>
        {
            specializations.ToTable("player_specializations");

            specializations.WithOwner()
                .HasForeignKey(p => p.PlayerId);

            specializations.HasKey(x => new
            {
                x.PlayerId,
                x.SpecializationType,
            });

            specializations.Property(p => p.SpecializationType)
                .HasConversion<int>()
                .IsRequired();

            specializations.HasIndex(p => p.SpecializationType);
        });

        builder.OwnsMany(p => p.Titles, titles =>
        {
            titles.ToTable("player_titles");

            titles.WithOwner()
                .HasForeignKey(p => p.PlayerId);

            titles.HasKey(x => new
            {
                x.PlayerId,
                x.TitleType,
            });

            titles.Property(p => p.TitleType)
                .HasConversion<int>()
                .IsRequired();

            titles.Property(p => p.IsCurrent)
                .IsRequired();

            titles.Property(p => p.UnlockedAtUtc)
                .IsRequired();

            titles.HasIndex(p => p.TitleType);

            titles.HasIndex(p => p.IsCurrent);
        });
    }
}

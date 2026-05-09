using CareerQuest.Modules.Players.Domain.Players;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CareerQuest.Modules.Players.Infrastructure.Players;

internal sealed class PlayerConfiguration : IEntityTypeConfiguration<Player>
{
    public void Configure(EntityTypeBuilder<Player> builder)
    {
        builder.ToTable("players");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.DisplayName)
            .HasMaxLength(400)
            .IsRequired();

        builder.Property(x => x.Email)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(x => x.Headline)
            .HasMaxLength(500);

        builder.Property(x => x.CareerStage)
            .HasConversion<string>()
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.JoinedAtUtc)
            .IsRequired();

        builder.Property(x => x.LastActiveAtUtc)
            .IsRequired();

        builder.Property(x => x.AvatarUrl)
            .HasConversion(
                x => x == null ? null : x.ToString(),
                x => string.IsNullOrWhiteSpace(x)
                    ? null
                    : new Uri(x))
            .HasMaxLength(2048);

        builder.HasIndex(x => x.Email)
            .IsUnique();

        builder.HasIndex(x => x.DisplayName);

        builder.HasIndex(x => x.CareerStage);

        builder.Ignore(x => x.CurrentTitle);

        builder.OwnsOne(x => x.Progression, progression =>
        {
            progression.ToTable("player_progressions");

            progression.WithOwner()
                .HasForeignKey(x => x.PlayerId);

            progression.HasKey(x => x.PlayerId);

            progression.Property(x => x.CurrentLevel)
                .IsRequired();

            progression.Property(x => x.CurrentXp)
                .IsRequired();

            progression.Property(x => x.TotalXp)
                .IsRequired();

            progression.Property(x => x.XpToNextLevel)
                .IsRequired();

            progression.Property(x => x.SkillPoints)
                .IsRequired();

            progression.HasIndex(x => x.CurrentLevel);

            progression.HasIndex(x => x.TotalXp);
        });

        builder.OwnsOne(x => x.Statistics, statistics =>
        {
            statistics.ToTable("player_statistics");

            statistics.WithOwner()
                .HasForeignKey(x => x.PlayerId);

            statistics.HasKey(x => x.PlayerId);

            statistics.Property(x => x.TotalPostsPublished)
                .IsRequired();

            statistics.Property(x => x.TotalCommits)
                .IsRequired();

            statistics.Property(x => x.TotalApplications)
                .IsRequired();

            statistics.Property(x => x.TotalNetworkingInteractions)
                .IsRequired();

            statistics.Property(x => x.TotalQuestsCompleted)
                .IsRequired();

            statistics.Property(x => x.TotalAchievementsUnlocked)
                .IsRequired();
        });

        builder.OwnsOne(x => x.Streak, streak =>
        {
            streak.ToTable("player_streaks");

            streak.WithOwner()
                .HasForeignKey(x => x.PlayerId);

            streak.HasKey(x => x.PlayerId);

            streak.Property(x => x.CurrentDays)
                .IsRequired();

            streak.Property(x => x.LongestDays)
                .IsRequired();

            streak.Property(x => x.CurrentMultiplier)
                .HasPrecision(5, 2);

            streak.Property(x => x.LastActivityDateUtc)
                .IsRequired();
        });

        builder.OwnsMany(x => x.Classes, classes =>
        {
            classes.ToTable("player_classes");

            classes.WithOwner()
                .HasForeignKey(x => x.PlayerId);

            classes.HasKey(x => new
            {
                x.PlayerId,
                x.ClassType,
            });

            classes.Property(x => x.ClassType)
                .HasConversion<int>()
                .IsRequired();

            classes.HasIndex(x => x.ClassType);
        });

        builder.OwnsMany(x => x.Specializations, specializations =>
        {
            specializations.ToTable("player_specializations");

            specializations.WithOwner()
                .HasForeignKey(x => x.PlayerId);

            specializations.HasKey(x => new
            {
                x.PlayerId,
                x.SpecializationType,
            });

            specializations.Property(x => x.SpecializationType)
                .HasConversion<int>()
                .IsRequired();

            specializations.HasIndex(x => x.SpecializationType);
        });

        builder.OwnsMany(x => x.Titles, titles =>
        {
            titles.ToTable("player_titles");

            titles.WithOwner()
                .HasForeignKey(x => x.PlayerId);

            titles.HasKey(x => new
            {
                x.PlayerId,
                x.TitleType,
            });

            titles.Property(x => x.TitleType)
                .HasConversion<int>()
                .IsRequired();

            titles.Property(x => x.IsCurrent)
                .IsRequired();

            titles.Property(x => x.UnlockedAtUtc)
                .IsRequired();

            titles.HasIndex(x => x.TitleType);

            titles.HasIndex(x => x.IsCurrent);
        });
    }
}

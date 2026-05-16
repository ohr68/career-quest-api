using System.Data.Common;
using CareerQuest.Common.Application.Data;
using CareerQuest.Common.Application.Messaging;
using CareerQuest.Common.Domain.Abstractions;
using CareerQuest.Modules.Players.Domain.Players;
using Dapper;

namespace CareerQuest.Modules.Players.Application.Players.GetPlayer;

internal sealed class GetPlayerQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetPlayerQuery, PlayerResponse>
{
    public async Task<Result<PlayerResponse>> Handle(
        GetPlayerQuery request,
        CancellationToken cancellationToken)
    {
        await using DbConnection connection =
            await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            """
            SELECT
                p.id AS PlayerId,
                p.display_name AS DisplayName,
                p.email AS Email,
                p.avatar_url AS AvatarUrl,
                p.headline AS Headline,
                p.career_stage AS CareerStage,
                p.joined_at_utc AS JoinedAtUtc,
                p.last_active_at_utc AS LastActiveAtUtc,

                pp.current_level AS CurrentLevel,
                pp.current_xp AS CurrentXp,
                pp.total_xp AS TotalXp,
                pp.xp_to_next_level AS XpToNextLevel,
                pp.skill_points AS SkillPoints,

                ps.total_posts_published AS TotalPostsPublished,
                ps.total_commits AS TotalCommits,
                ps.total_applications AS TotalApplications,
                ps.total_networking_interactions AS TotalNetworkingInteractions,
                ps.total_quests_completed AS TotalQuestsCompleted,
                ps.total_achievements_unlocked AS TotalAchievementsUnlocked,

                pst.current_days AS CurrentDays,
                pst.longest_days AS LongestDays,
                pst.current_multiplier AS CurrentMultiplier,
                pst.last_activity_date_utc AS LastActivityDateUtc,

                pc.class_type AS ClassType,
                psp.specialization_type AS SpecializationType

            FROM players.players p

            LEFT JOIN players.player_progressions pp
                ON pp.player_id = p.id

            LEFT JOIN players.player_statistics ps
                ON ps.id = p.id

            LEFT JOIN players.player_streaks pst
                ON pst.id = p.id

            LEFT JOIN players.player_classes pc
                ON pc.player_id = p.id

            LEFT JOIN players.player_specializations psp
                ON psp.player_id = p.id

            WHERE p.id = @PlayerId
            """;

        Dictionary<Guid, PlayerResponse> players = [];

        await connection.QueryAsync<
            PlayerRow,
            PlayerProgression?,
            PlayerStatistics?,
            PlayerStreak?,
            PlayerClassRow?,
            PlayerSpecializationRow?,
            PlayerResponse>(
            sql,
            (playerRow,
                progression,
                statistics,
                streak,
                playerClass,
                specialization) =>
            {
                if (!players.TryGetValue(playerRow.PlayerId, out PlayerResponse? player))
                {
                    player = new PlayerResponse(
                        playerRow.PlayerId,
                        playerRow.DisplayName,
                        playerRow.Email,
                        string.IsNullOrWhiteSpace(playerRow.AvatarUrl)
                            ? null
                            : new Uri(playerRow.AvatarUrl),
                        playerRow.Headline,
                        Enum.Parse<CareerStage>(playerRow.CareerStage),
                        playerRow.JoinedAtUtc,
                        playerRow.LastActiveAtUtc,
                        progression,
                        statistics,
                        streak,
                        [],
                        []);

                    players.Add(player.PlayerId, player);
                }

                var classes =
                    player.Classes.ToList();

                if (playerClass is not null &&
                    !classes.Contains(playerClass.ClassType))
                {
                    classes.Add(playerClass.ClassType);
                }

                var specializations =
                    player.Specializations.ToList();

                if (specialization is not null &&
                    !specializations.Contains(specialization.SpecializationType))
                {
                    specializations.Add(specialization.SpecializationType);
                }

                players[player.PlayerId] = player with
                {
                    Classes = classes,
                    Specializations = specializations,
                };

                return player;
            },
            request,
            splitOn:
            """
            CurrentLevel,
            TotalPostsPublished,
            CurrentDays,
            ClassType,
            SpecializationType
            """);

        PlayerResponse? result =
            players.Values.SingleOrDefault();

        return result ?? Result.Failure<PlayerResponse>(
            PlayerErrors.NotFound(request.PlayerId));
    }

    private sealed class PlayerRow
    {
        public Guid PlayerId { get; init; } = Guid.Empty;

        public string DisplayName { get; init; } = null!;

        public string Email { get; init; } = null!;

        public string AvatarUrl { get; init; } = null!;

        public string Headline { get; init; } = null!;

        public string CareerStage { get; init; } = null!;

        public DateTime JoinedAtUtc { get; init; } = DateTime.MinValue;

        public DateTime LastActiveAtUtc { get; init; } = DateTime.MinValue;
    }

    private sealed record PlayerClassRow(
        PlayerClassType ClassType);

    private sealed record PlayerSpecializationRow(
        PlayerSpecializationType SpecializationType);
}

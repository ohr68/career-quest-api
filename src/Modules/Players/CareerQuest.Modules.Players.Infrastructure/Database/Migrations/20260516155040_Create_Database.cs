using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CareerQuest.Modules.Players.Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class Create_Database : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: "players");

        migrationBuilder.CreateTable(
            name: "inbox_message_consumers",
            schema: "players",
            columns: table => new
            {
                inbox_message_id = table.Column<Guid>(type: "uuid", nullable: false),
                name = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_inbox_message_consumers", x => new { x.inbox_message_id, x.name });
            });

        migrationBuilder.CreateTable(
            name: "inbox_messages",
            schema: "players",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                type = table.Column<string>(type: "text", nullable: false),
                content = table.Column<string>(type: "jsonb", maxLength: 2000, nullable: false),
                occurred_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                processed_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                error = table.Column<string>(type: "text", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_inbox_messages", x => x.id);
            });

        migrationBuilder.CreateTable(
            name: "outbox_message_consumers",
            schema: "players",
            columns: table => new
            {
                outbox_message_id = table.Column<Guid>(type: "uuid", nullable: false),
                name = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_outbox_message_consumers", x => new { x.outbox_message_id, x.name });
            });

        migrationBuilder.CreateTable(
            name: "outbox_messages",
            schema: "players",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                type = table.Column<string>(type: "text", nullable: false),
                content = table.Column<string>(type: "jsonb", maxLength: 2000, nullable: false),
                occurred_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                processed_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                error = table.Column<string>(type: "text", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_outbox_messages", x => x.id);
            });

        migrationBuilder.CreateTable(
            name: "players",
            schema: "players",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                display_name = table.Column<string>(type: "character varying(400)", maxLength: 400, nullable: false),
                email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                avatar_url = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: true),
                headline = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                career_stage = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                joined_at_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                last_active_at_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_players", x => x.id);
            });

        migrationBuilder.CreateTable(
            name: "player_classes",
            schema: "players",
            columns: table => new
            {
                player_id = table.Column<Guid>(type: "uuid", nullable: false),
                class_type = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_player_classes", x => new { x.player_id, x.class_type });
                table.ForeignKey(
                    name: "fk_player_classes_players_player_id",
                    column: x => x.player_id,
                    principalSchema: "players",
                    principalTable: "players",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "player_progressions",
            schema: "players",
            columns: table => new
            {
                player_id = table.Column<Guid>(type: "uuid", nullable: false),
                current_level = table.Column<int>(type: "integer", nullable: false),
                current_xp = table.Column<int>(type: "integer", nullable: false),
                total_xp = table.Column<int>(type: "integer", nullable: false),
                xp_to_next_level = table.Column<int>(type: "integer", nullable: false),
                skill_points = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_player_progressions", x => x.player_id);
                table.ForeignKey(
                    name: "fk_player_progressions_players_player_id",
                    column: x => x.player_id,
                    principalSchema: "players",
                    principalTable: "players",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "player_specializations",
            schema: "players",
            columns: table => new
            {
                player_id = table.Column<Guid>(type: "uuid", nullable: false),
                specialization_type = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_player_specializations", x => new { x.player_id, x.specialization_type });
                table.ForeignKey(
                    name: "fk_player_specializations_players_player_id",
                    column: x => x.player_id,
                    principalSchema: "players",
                    principalTable: "players",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "player_statistics",
            schema: "players",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                total_posts_published = table.Column<int>(type: "integer", nullable: false),
                total_commits = table.Column<int>(type: "integer", nullable: false),
                total_applications = table.Column<int>(type: "integer", nullable: false),
                total_networking_interactions = table.Column<int>(type: "integer", nullable: false),
                total_quests_completed = table.Column<int>(type: "integer", nullable: false),
                total_achievements_unlocked = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_player_statistics", x => x.id);
                table.ForeignKey(
                    name: "fk_player_statistics_players_id",
                    column: x => x.id,
                    principalSchema: "players",
                    principalTable: "players",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "player_streaks",
            schema: "players",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                current_days = table.Column<int>(type: "integer", nullable: false),
                longest_days = table.Column<int>(type: "integer", nullable: false),
                current_multiplier = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: false),
                last_activity_date_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_player_streaks", x => x.id);
                table.ForeignKey(
                    name: "fk_player_streaks_players_id",
                    column: x => x.id,
                    principalSchema: "players",
                    principalTable: "players",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "player_titles",
            schema: "players",
            columns: table => new
            {
                player_id = table.Column<Guid>(type: "uuid", nullable: false),
                title_type = table.Column<int>(type: "integer", nullable: false),
                is_current = table.Column<bool>(type: "boolean", nullable: false),
                unlocked_at_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_player_titles", x => new { x.player_id, x.title_type });
                table.ForeignKey(
                    name: "fk_player_titles_players_player_id",
                    column: x => x.player_id,
                    principalSchema: "players",
                    principalTable: "players",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "player_xp_transactions",
            schema: "players",
            columns: table => new
            {
                player_id = table.Column<Guid>(type: "uuid", nullable: false),
                id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                action = table.Column<string>(type: "text", nullable: false),
                amount = table.Column<int>(type: "integer", nullable: false),
                multiplier = table.Column<float>(type: "real", nullable: false),
                earned_at_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                notes = table.Column<string>(type: "text", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_player_xp_transactions", x => new { x.player_id, x.id });
                table.ForeignKey(
                    name: "fk_player_xp_transactions_player_progressions_player_id",
                    column: x => x.player_id,
                    principalSchema: "players",
                    principalTable: "player_progressions",
                    principalColumn: "player_id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "ix_player_classes_class_type",
            schema: "players",
            table: "player_classes",
            column: "class_type");

        migrationBuilder.CreateIndex(
            name: "ix_player_progressions_current_level",
            schema: "players",
            table: "player_progressions",
            column: "current_level");

        migrationBuilder.CreateIndex(
            name: "ix_player_progressions_total_xp",
            schema: "players",
            table: "player_progressions",
            column: "total_xp");

        migrationBuilder.CreateIndex(
            name: "ix_player_specializations_specialization_type",
            schema: "players",
            table: "player_specializations",
            column: "specialization_type");

        migrationBuilder.CreateIndex(
            name: "ix_player_titles_is_current",
            schema: "players",
            table: "player_titles",
            column: "is_current");

        migrationBuilder.CreateIndex(
            name: "ix_player_titles_title_type",
            schema: "players",
            table: "player_titles",
            column: "title_type");

        migrationBuilder.CreateIndex(
            name: "ix_players_career_stage",
            schema: "players",
            table: "players",
            column: "career_stage");

        migrationBuilder.CreateIndex(
            name: "ix_players_display_name",
            schema: "players",
            table: "players",
            column: "display_name");

        migrationBuilder.CreateIndex(
            name: "ix_players_email",
            schema: "players",
            table: "players",
            column: "email",
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "inbox_message_consumers",
            schema: "players");

        migrationBuilder.DropTable(
            name: "inbox_messages",
            schema: "players");

        migrationBuilder.DropTable(
            name: "outbox_message_consumers",
            schema: "players");

        migrationBuilder.DropTable(
            name: "outbox_messages",
            schema: "players");

        migrationBuilder.DropTable(
            name: "player_classes",
            schema: "players");

        migrationBuilder.DropTable(
            name: "player_specializations",
            schema: "players");

        migrationBuilder.DropTable(
            name: "player_statistics",
            schema: "players");

        migrationBuilder.DropTable(
            name: "player_streaks",
            schema: "players");

        migrationBuilder.DropTable(
            name: "player_titles",
            schema: "players");

        migrationBuilder.DropTable(
            name: "player_xp_transactions",
            schema: "players");

        migrationBuilder.DropTable(
            name: "player_progressions",
            schema: "players");

        migrationBuilder.DropTable(
            name: "players",
            schema: "players");
    }
}

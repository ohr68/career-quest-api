using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CareerQuest.Modules.Users.Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class Create_Database : Migration
{
    private static readonly string[] columns = new[] { "permission_code", "role_name" };

    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: "users");

        migrationBuilder.CreateTable(
            name: "inbox_message_consumers",
            schema: "users",
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
            schema: "users",
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
            schema: "users",
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
            schema: "users",
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
            name: "permissions",
            schema: "users",
            columns: table => new
            {
                code = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_permissions", x => x.code);
            });

        migrationBuilder.CreateTable(
            name: "roles",
            schema: "users",
            columns: table => new
            {
                name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_roles", x => x.name);
            });

        migrationBuilder.CreateTable(
            name: "users",
            schema: "users",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                email = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                first_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                last_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                identity_id = table.Column<string>(type: "text", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_users", x => x.id);
            });

        migrationBuilder.CreateTable(
            name: "role_permissions",
            schema: "users",
            columns: table => new
            {
                permission_code = table.Column<string>(type: "character varying(100)", nullable: false),
                role_name = table.Column<string>(type: "character varying(50)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_role_permissions", x => new { x.permission_code, x.role_name });
                table.ForeignKey(
                    name: "fk_role_permissions_permissions_permission_code",
                    column: x => x.permission_code,
                    principalSchema: "users",
                    principalTable: "permissions",
                    principalColumn: "code",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "fk_role_permissions_roles_role_name",
                    column: x => x.role_name,
                    principalSchema: "users",
                    principalTable: "roles",
                    principalColumn: "name",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "user_roles",
            schema: "users",
            columns: table => new
            {
                roles_name = table.Column<string>(type: "character varying(50)", nullable: false),
                user_id = table.Column<Guid>(type: "uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_user_roles", x => new { x.roles_name, x.user_id });
                table.ForeignKey(
                    name: "fk_user_roles_roles_roles_name",
                    column: x => x.roles_name,
                    principalSchema: "users",
                    principalTable: "roles",
                    principalColumn: "name",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "fk_user_roles_users_user_id",
                    column: x => x.user_id,
                    principalSchema: "users",
                    principalTable: "users",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.InsertData(
            schema: "users",
            table: "permissions",
            column: "code",
            values: new object[]
            {
                "achievements:read",
                "achievements:unlock",
                "activities:create",
                "activities:read",
                "activities:update",
                "analytics:read",
                "boss-battles:complete",
                "boss-battles:read",
                "career-stages:read",
                "career-stages:update",
                "clients:create",
                "clients:read",
                "clients:update",
                "content:publish",
                "content:read",
                "content:update",
                "dashboard:read",
                "networking:create",
                "networking:read",
                "opportunities:create",
                "opportunities:delete",
                "opportunities:read",
                "opportunities:update",
                "permissions:manage",
                "progression:earn-xp",
                "progression:read",
                "progression:spend-skill-points",
                "progression:update",
                "quests:complete",
                "quests:create",
                "quests:read",
                "quests:update",
                "roles:manage",
                "skill-trees:read",
                "skill-trees:reset",
                "skill-trees:unlock",
                "user-classes:add",
                "user-classes:read",
                "user-classes:remove",
                "user-profiles:read",
                "user-profiles:update",
                "user-specializations:add",
                "user-specializations:read",
                "user-specializations:remove",
                "users:create",
                "users:delete",
                "users:read",
                "users:search",
                "users:update",
                "weekly-reviews:create",
                "weekly-reviews:read"
            });

        migrationBuilder.InsertData(
            schema: "users",
            table: "roles",
            column: "name",
            values: new object[]
            {
                "Administrator",
                "Member"
            });

        migrationBuilder.InsertData(
            schema: "users",
            table: "role_permissions",
            columns: columns,
            values: new object[,]
            {
                { "achievements:read", "Administrator" },
                { "achievements:read", "Member" },
                { "achievements:unlock", "Administrator" },
                { "activities:create", "Administrator" },
                { "activities:create", "Member" },
                { "activities:read", "Administrator" },
                { "activities:read", "Member" },
                { "activities:update", "Administrator" },
                { "analytics:read", "Administrator" },
                { "boss-battles:complete", "Administrator" },
                { "boss-battles:complete", "Member" },
                { "boss-battles:read", "Administrator" },
                { "boss-battles:read", "Member" },
                { "career-stages:read", "Administrator" },
                { "career-stages:read", "Member" },
                { "career-stages:update", "Administrator" },
                { "career-stages:update", "Member" },
                { "clients:create", "Administrator" },
                { "clients:read", "Administrator" },
                { "clients:update", "Administrator" },
                { "content:publish", "Administrator" },
                { "content:publish", "Member" },
                { "content:read", "Administrator" },
                { "content:read", "Member" },
                { "content:update", "Administrator" },
                { "dashboard:read", "Administrator" },
                { "dashboard:read", "Member" },
                { "networking:create", "Administrator" },
                { "networking:create", "Member" },
                { "networking:read", "Administrator" },
                { "networking:read", "Member" },
                { "opportunities:create", "Administrator" },
                { "opportunities:create", "Member" },
                { "opportunities:delete", "Administrator" },
                { "opportunities:read", "Administrator" },
                { "opportunities:read", "Member" },
                { "opportunities:update", "Administrator" },
                { "permissions:manage", "Administrator" },
                { "progression:earn-xp", "Administrator" },
                { "progression:read", "Administrator" },
                { "progression:read", "Member" },
                { "progression:spend-skill-points", "Administrator" },
                { "progression:update", "Administrator" },
                { "quests:complete", "Administrator" },
                { "quests:complete", "Member" },
                { "quests:create", "Administrator" },
                { "quests:read", "Administrator" },
                { "quests:read", "Member" },
                { "quests:update", "Administrator" },
                { "roles:manage", "Administrator" },
                { "skill-trees:read", "Administrator" },
                { "skill-trees:read", "Member" },
                { "skill-trees:reset", "Administrator" },
                { "skill-trees:unlock", "Administrator" },
                { "skill-trees:unlock", "Member" },
                { "user-classes:add", "Administrator" },
                { "user-classes:add", "Member" },
                { "user-classes:read", "Administrator" },
                { "user-classes:read", "Member" },
                { "user-classes:remove", "Administrator" },
                { "user-profiles:read", "Administrator" },
                { "user-profiles:read", "Member" },
                { "user-profiles:update", "Administrator" },
                { "user-profiles:update", "Member" },
                { "user-specializations:add", "Administrator" },
                { "user-specializations:add", "Member" },
                { "user-specializations:read", "Administrator" },
                { "user-specializations:read", "Member" },
                { "user-specializations:remove", "Administrator" },
                { "users:create", "Administrator" },
                { "users:delete", "Administrator" },
                { "users:read", "Administrator" },
                { "users:read", "Member" },
                { "users:search", "Administrator" },
                { "users:update", "Administrator" },
                { "users:update", "Member" },
                { "weekly-reviews:create", "Administrator" },
                { "weekly-reviews:create", "Member" },
                { "weekly-reviews:read", "Administrator" },
                { "weekly-reviews:read", "Member" }
            });

        migrationBuilder.CreateIndex(
            name: "ix_role_permissions_role_name",
            schema: "users",
            table: "role_permissions",
            column: "role_name");

        migrationBuilder.CreateIndex(
            name: "ix_user_roles_user_id",
            schema: "users",
            table: "user_roles",
            column: "user_id");

        migrationBuilder.CreateIndex(
            name: "ix_users_email",
            schema: "users",
            table: "users",
            column: "email",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "ix_users_identity_id",
            schema: "users",
            table: "users",
            column: "identity_id",
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "inbox_message_consumers",
            schema: "users");

        migrationBuilder.DropTable(
            name: "inbox_messages",
            schema: "users");

        migrationBuilder.DropTable(
            name: "outbox_message_consumers",
            schema: "users");

        migrationBuilder.DropTable(
            name: "outbox_messages",
            schema: "users");

        migrationBuilder.DropTable(
            name: "role_permissions",
            schema: "users");

        migrationBuilder.DropTable(
            name: "user_roles",
            schema: "users");

        migrationBuilder.DropTable(
            name: "permissions",
            schema: "users");

        migrationBuilder.DropTable(
            name: "roles",
            schema: "users");

        migrationBuilder.DropTable(
            name: "users",
            schema: "users");
    }
}

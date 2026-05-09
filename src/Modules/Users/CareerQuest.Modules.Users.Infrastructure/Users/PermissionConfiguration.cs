using CareerQuest.Modules.Users.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CareerQuest.Modules.Users.Infrastructure.Users;

internal sealed class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable("permissions");

        builder.HasKey(x => x.Code);

        builder.Property(x => x.Code)
            .HasMaxLength(100);

        builder.HasData(
            // Users
            Permission.GetUser,
            Permission.SearchUsers,
            Permission.RegisterUser,
            Permission.ModifyUser,
            Permission.DeleteUser,

            // User Profile
            Permission.GetUserProfile,
            Permission.ModifyUserProfile,

            // User Classes
            Permission.GetUserClasses,
            Permission.AddUserClass,
            Permission.RemoveUserClass,

            // User Specializations
            Permission.GetUserSpecializations,
            Permission.AddUserSpecialization,
            Permission.RemoveUserSpecialization,

            // Career
            Permission.GetCareerStage,
            Permission.AdvanceCareerStage,

            // Progression
            Permission.GetProgression,
            Permission.EarnXp,
            Permission.ModifyProgression,
            Permission.SpendSkillPoints,

            // Quests
            Permission.GetQuests,
            Permission.CompleteQuest,
            Permission.CreateQuest,
            Permission.ModifyQuest,

            // Boss Battles
            Permission.GetBossBattles,
            Permission.CompleteBossBattle,

            // Achievements
            Permission.GetAchievements,
            Permission.UnlockAchievement,

            // Skills
            Permission.GetSkillTrees,
            Permission.UnlockSkill,
            Permission.ResetSkills,

            // Activities
            Permission.GetActivities,
            Permission.RegisterActivity,
            Permission.ModifyActivity,

            // Networking
            Permission.GetNetworkingInteractions,
            Permission.RegisterNetworkingInteraction,

            // Content
            Permission.GetContent,
            Permission.PublishContent,
            Permission.ModifyContent,

            // Opportunities
            Permission.GetOpportunities,
            Permission.CreateOpportunity,
            Permission.ModifyOpportunity,
            Permission.DeleteOpportunity,

            // Clients
            Permission.GetClients,
            Permission.CreateClient,
            Permission.ModifyClient,

            // Reviews
            Permission.GetWeeklyReviews,
            Permission.CreateWeeklyReview,

            // Dashboard
            Permission.GetDashboard,
            Permission.GetAnalytics,

            // Admin
            Permission.ManageRoles,
            Permission.ManagePermissions);

        builder
            .HasMany<Role>()
            .WithMany()
            .UsingEntity(joinBuilder =>
            {
                joinBuilder.ToTable("role_permissions");

                joinBuilder.HasData(

                    // =====================================================
                    // MEMBER
                    // =====================================================

                    CreateRolePermission(Role.Member, Permission.GetUser),
                    CreateRolePermission(Role.Member, Permission.ModifyUser),

                    CreateRolePermission(Role.Member, Permission.GetUserProfile),
                    CreateRolePermission(Role.Member, Permission.ModifyUserProfile),

                    CreateRolePermission(Role.Member, Permission.GetUserClasses),
                    CreateRolePermission(Role.Member, Permission.AddUserClass),

                    CreateRolePermission(Role.Member, Permission.GetUserSpecializations),
                    CreateRolePermission(Role.Member, Permission.AddUserSpecialization),

                    CreateRolePermission(Role.Member, Permission.GetCareerStage),
                    CreateRolePermission(Role.Member, Permission.AdvanceCareerStage),

                    CreateRolePermission(Role.Member, Permission.GetProgression),

                    CreateRolePermission(Role.Member, Permission.GetQuests),
                    CreateRolePermission(Role.Member, Permission.CompleteQuest),

                    CreateRolePermission(Role.Member, Permission.GetBossBattles),
                    CreateRolePermission(Role.Member, Permission.CompleteBossBattle),

                    CreateRolePermission(Role.Member, Permission.GetAchievements),

                    CreateRolePermission(Role.Member, Permission.GetSkillTrees),
                    CreateRolePermission(Role.Member, Permission.UnlockSkill),

                    CreateRolePermission(Role.Member, Permission.GetActivities),
                    CreateRolePermission(Role.Member, Permission.RegisterActivity),

                    CreateRolePermission(Role.Member, Permission.GetNetworkingInteractions),
                    CreateRolePermission(Role.Member, Permission.RegisterNetworkingInteraction),

                    CreateRolePermission(Role.Member, Permission.GetContent),
                    CreateRolePermission(Role.Member, Permission.PublishContent),

                    CreateRolePermission(Role.Member, Permission.GetOpportunities),
                    CreateRolePermission(Role.Member, Permission.CreateOpportunity),

                    CreateRolePermission(Role.Member, Permission.GetWeeklyReviews),
                    CreateRolePermission(Role.Member, Permission.CreateWeeklyReview),

                    CreateRolePermission(Role.Member, Permission.GetDashboard),

                    // =====================================================
                    // ADMINISTRATOR
                    // =====================================================

                    CreateRolePermission(Role.Administrator, Permission.GetUser),
                    CreateRolePermission(Role.Administrator, Permission.SearchUsers),
                    CreateRolePermission(Role.Administrator, Permission.RegisterUser),
                    CreateRolePermission(Role.Administrator, Permission.ModifyUser),
                    CreateRolePermission(Role.Administrator, Permission.DeleteUser),

                    CreateRolePermission(Role.Administrator, Permission.GetUserProfile),
                    CreateRolePermission(Role.Administrator, Permission.ModifyUserProfile),

                    CreateRolePermission(Role.Administrator, Permission.GetUserClasses),
                    CreateRolePermission(Role.Administrator, Permission.AddUserClass),
                    CreateRolePermission(Role.Administrator, Permission.RemoveUserClass),

                    CreateRolePermission(Role.Administrator, Permission.GetUserSpecializations),
                    CreateRolePermission(Role.Administrator, Permission.AddUserSpecialization),
                    CreateRolePermission(Role.Administrator, Permission.RemoveUserSpecialization),

                    CreateRolePermission(Role.Administrator, Permission.GetCareerStage),
                    CreateRolePermission(Role.Administrator, Permission.AdvanceCareerStage),

                    CreateRolePermission(Role.Administrator, Permission.GetProgression),
                    CreateRolePermission(Role.Administrator, Permission.EarnXp),
                    CreateRolePermission(Role.Administrator, Permission.ModifyProgression),
                    CreateRolePermission(Role.Administrator, Permission.SpendSkillPoints),

                    CreateRolePermission(Role.Administrator, Permission.GetQuests),
                    CreateRolePermission(Role.Administrator, Permission.CompleteQuest),
                    CreateRolePermission(Role.Administrator, Permission.CreateQuest),
                    CreateRolePermission(Role.Administrator, Permission.ModifyQuest),

                    CreateRolePermission(Role.Administrator, Permission.GetBossBattles),
                    CreateRolePermission(Role.Administrator, Permission.CompleteBossBattle),

                    CreateRolePermission(Role.Administrator, Permission.GetAchievements),
                    CreateRolePermission(Role.Administrator, Permission.UnlockAchievement),

                    CreateRolePermission(Role.Administrator, Permission.GetSkillTrees),
                    CreateRolePermission(Role.Administrator, Permission.UnlockSkill),
                    CreateRolePermission(Role.Administrator, Permission.ResetSkills),

                    CreateRolePermission(Role.Administrator, Permission.GetActivities),
                    CreateRolePermission(Role.Administrator, Permission.RegisterActivity),
                    CreateRolePermission(Role.Administrator, Permission.ModifyActivity),

                    CreateRolePermission(Role.Administrator, Permission.GetNetworkingInteractions),
                    CreateRolePermission(Role.Administrator, Permission.RegisterNetworkingInteraction),

                    CreateRolePermission(Role.Administrator, Permission.GetContent),
                    CreateRolePermission(Role.Administrator, Permission.PublishContent),
                    CreateRolePermission(Role.Administrator, Permission.ModifyContent),

                    CreateRolePermission(Role.Administrator, Permission.GetOpportunities),
                    CreateRolePermission(Role.Administrator, Permission.CreateOpportunity),
                    CreateRolePermission(Role.Administrator, Permission.ModifyOpportunity),
                    CreateRolePermission(Role.Administrator, Permission.DeleteOpportunity),

                    CreateRolePermission(Role.Administrator, Permission.GetClients),
                    CreateRolePermission(Role.Administrator, Permission.CreateClient),
                    CreateRolePermission(Role.Administrator, Permission.ModifyClient),

                    CreateRolePermission(Role.Administrator, Permission.GetWeeklyReviews),
                    CreateRolePermission(Role.Administrator, Permission.CreateWeeklyReview),

                    CreateRolePermission(Role.Administrator, Permission.GetDashboard),
                    CreateRolePermission(Role.Administrator, Permission.GetAnalytics),

                    CreateRolePermission(Role.Administrator, Permission.ManageRoles),
                    CreateRolePermission(Role.Administrator, Permission.ManagePermissions)
                );
            });
    }

    private static object CreateRolePermission(
        Role role,
        Permission permission)
    {
        return new
        {
            RoleName = role.Name,
            PermissionCode = permission.Code
        };
    }
}

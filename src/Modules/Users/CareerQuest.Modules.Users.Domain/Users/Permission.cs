namespace CareerQuest.Modules.Users.Domain.Users;

public sealed class Permission
{
    // Users
    public static readonly Permission GetUser =
        new("users:read");

    public static readonly Permission SearchUsers =
        new("users:search");

    public static readonly Permission RegisterUser =
        new("users:create");

    public static readonly Permission ModifyUser =
        new("users:update");

    public static readonly Permission DeleteUser =
        new("users:delete");

    // User Profile
    public static readonly Permission GetUserProfile =
        new("user-profiles:read");

    public static readonly Permission ModifyUserProfile =
        new("user-profiles:update");

    // User Classes
    public static readonly Permission GetUserClasses =
        new("user-classes:read");

    public static readonly Permission AddUserClass =
        new("user-classes:add");

    public static readonly Permission RemoveUserClass =
        new("user-classes:remove");

    // User Specializations
    public static readonly Permission GetUserSpecializations =
        new("user-specializations:read");

    public static readonly Permission AddUserSpecialization =
        new("user-specializations:add");

    public static readonly Permission RemoveUserSpecialization =
        new("user-specializations:remove");

    // Career
    public static readonly Permission GetCareerStage =
        new("career-stages:read");

    public static readonly Permission AdvanceCareerStage =
        new("career-stages:update");

    // Progression
    public static readonly Permission GetProgression =
        new("progression:read");

    public static readonly Permission EarnXp =
        new("progression:earn-xp");

    public static readonly Permission ModifyProgression =
        new("progression:update");

    public static readonly Permission SpendSkillPoints =
        new("progression:spend-skill-points");

    // Quests
    public static readonly Permission GetQuests =
        new("quests:read");

    public static readonly Permission CompleteQuest =
        new("quests:complete");

    public static readonly Permission CreateQuest =
        new("quests:create");

    public static readonly Permission ModifyQuest =
        new("quests:update");

    // Boss Battles
    public static readonly Permission GetBossBattles =
        new("boss-battles:read");

    public static readonly Permission CompleteBossBattle =
        new("boss-battles:complete");

    // Achievements
    public static readonly Permission GetAchievements =
        new("achievements:read");

    public static readonly Permission UnlockAchievement =
        new("achievements:unlock");

    // Skills
    public static readonly Permission GetSkillTrees =
        new("skill-trees:read");

    public static readonly Permission UnlockSkill =
        new("skill-trees:unlock");

    public static readonly Permission ResetSkills =
        new("skill-trees:reset");

    // Activities / Visibility
    public static readonly Permission GetActivities =
        new("activities:read");

    public static readonly Permission RegisterActivity =
        new("activities:create");

    public static readonly Permission ModifyActivity =
        new("activities:update");

    // Networking
    public static readonly Permission GetNetworkingInteractions =
        new("networking:read");

    public static readonly Permission RegisterNetworkingInteraction =
        new("networking:create");

    // Content
    public static readonly Permission GetContent =
        new("content:read");

    public static readonly Permission PublishContent =
        new("content:publish");

    public static readonly Permission ModifyContent =
        new("content:update");

    // Opportunities
    public static readonly Permission GetOpportunities =
        new("opportunities:read");

    public static readonly Permission CreateOpportunity =
        new("opportunities:create");

    public static readonly Permission ModifyOpportunity =
        new("opportunities:update");

    public static readonly Permission DeleteOpportunity =
        new("opportunities:delete");

    // Clients
    public static readonly Permission GetClients =
        new("clients:read");

    public static readonly Permission CreateClient =
        new("clients:create");

    public static readonly Permission ModifyClient =
        new("clients:update");

    // Reviews
    public static readonly Permission GetWeeklyReviews =
        new("weekly-reviews:read");

    public static readonly Permission CreateWeeklyReview =
        new("weekly-reviews:create");

    // Dashboard
    public static readonly Permission GetDashboard =
        new("dashboard:read");

    public static readonly Permission GetAnalytics =
        new("analytics:read");

    // Admin
    public static readonly Permission ManageRoles =
        new("roles:manage");

    public static readonly Permission ManagePermissions =
        new("permissions:manage");

    public Permission(string code)
    {
        Code = code;
    }

    public string Code { get; private set; }
}

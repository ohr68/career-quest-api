namespace CareerQuest.Modules.Players.Presentation;

public static class Permissions
{
    internal const string GetProfile = "user-profiles:read";
    internal const string CompleteProfile = "user-profiles:update";
    internal const string ReadClassed = "user-classes:read";
    internal const string AddClasses = "user-classes:add";
    internal const string RemoveClasses = "user-classes:remove";
    internal const string ReadSpecializations = "user-specializations:read";
    internal const string AddSpecialization = "user-specializations:add";
    internal const string RemoveSpecialization = "user-specializations:remove";

    // Career    
    internal const string ReadCareerStages = "career-stages:read";
    internal const string ModifyCareerStages = "career-stages:update";

    // Progression
    internal const string ReadProgression = "progression:read";
    internal const string EarnXp = "progression:earn-xp";
    internal const string ModifyProgression = "progression:update";
    internal const string SpendSkillPoints = "progression:spend-skill-points";

    // Quests
    internal const string ReadQuests = "quests:read";
    internal const string CompleteQuest = "quests:complete";
    internal const string CreateQuest = "quests:create";
    internal const string ModifyQuest = "quests:update";

    // Boss Battles
    internal const string ReadBossBattles = "boss-battles:read";
    internal const string CompleteBossBattle = "boss-battles:complete";

    // Achievements
    internal const string ReadAchievements = "achievements:read";
    internal const string UnlockAchievement = "achievements:unlock";

    // Skills
    internal const string ReadSkillTrees = "skill-trees:read";
    internal const string UnlockSkill = "skill-trees:unlock";
    internal const string ResetSkills = "skill-trees:reset";

    // Activities / Visibility
    internal const string ReadActivities = "activities:read";
    internal const string RegisterActivity = "activities:create";
    internal const string ModifyActivity = "activities:update";

    // Networking
    internal const string ReadNetworkingInteractions = "networking:read";
    internal const string RegisterNetworkingInteraction = "networking:create";

    // Content
    internal const string ReadContent = "content:read";
    internal const string PublishContent = "content:publish";
    internal const string ModifyContent = "content:update";
    internal const string ReadOpportunities = "opportunities:read";
    internal const string CreateOpportunity = "opportunities:create";

    // Opportunities
    internal const string ModifyOpportunity = "opportunities:update";
    internal const string DeleteOpportunity = "opportunities:delete";
    internal const string ReadClients = "clients:read";
    internal const string CreateClient = "clients:create";
    internal const string ModifyClient = "clients:update";

    // Reviews
    internal const string ReadWeeklyReviews = "weekly-reviews:read";
    internal const string CreateWeeklyReview = "weekly-reviews:create";

    // Dashboard
    internal const string ReadDashboard = "dashboard:read";
    internal const string ReadAnalytics = "analytics:read";
}

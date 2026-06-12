using CareerQuest.Common.Domain.Abstractions;

namespace CareerQuest.Modules.Players.Domain.Players;

public static class PlayerErrors
{
    public static Error InvalidSpecialization(int specializationId)
    {
        return Error.NotFound("Players.InvalidSpecialization",
            $"The specialization {specializationId} was not found.");
    }

    public static Error InvalidClass(int classId)
    {
        return Error.NotFound("Players.InvalidClass",
            $"The class {classId} was not found.");
    }

    public static Error NotFound(Guid playerId)
    {
        return Error.NotFound("Players.NotFound",
            $"The player with the identifier {playerId} was not found.");
    }

    public static Error ProfileNotCompleted(Guid playerId)
    {
        return Error.Problem("Players.ProfileNotCompleted",
            $"The player with the identifier {playerId} has not completed their profile.");
    }

    public static Error QuestNotFound(Guid questId)
    {
        return Error.NotFound("Players.QuestNotFound",
            $"The quest with the identifier {questId} was not found.");
    }

    public static Error QuestExpired(Guid questId)
    {
        return Error.Problem("Players.QuestExpired",
            $"The quest with the identifier {questId} has expired.");
    }
}

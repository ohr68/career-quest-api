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
}

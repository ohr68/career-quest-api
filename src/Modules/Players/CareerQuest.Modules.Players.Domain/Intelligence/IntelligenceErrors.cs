using CareerQuest.Common.Domain.Abstractions;

namespace CareerQuest.Modules.Players.Domain.Intelligence;

public static class IntelligenceErrors
{
    public static readonly Error Unavailable =
        Error.Failure("Intelligence.Unavailable", "The assistant is temporarily unavailable.");

    public static readonly Error InvalidResponse =
        Error.Failure("Intelligence.InvalidResponse", "The assistant returned an unreadable result.");
}

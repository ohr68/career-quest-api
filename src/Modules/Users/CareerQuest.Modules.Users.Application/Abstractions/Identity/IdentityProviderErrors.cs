using CareerQuest.Common.Domain.Abstractions;

namespace CareerQuest.Modules.Users.Application.Abstractions.Identity;

public static class IdentityProviderErrors
{
    public static readonly Error EmailIsNotUnique = Error.Conflict(
        "Identity.EmailIsNotUnique",
        "The specified email is not unique.");

    public static readonly Error EmailNotFound = Error.NotFound(
        "Identity.EmailNotFound",
        "The specified email was not found.");

    public static readonly Error InvalidRefreshToken = Error.Problem(
        "Identity.InvalidRefreshToken",
        "The specified refresh token is invalid.");
}

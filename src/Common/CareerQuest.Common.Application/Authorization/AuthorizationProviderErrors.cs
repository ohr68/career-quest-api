using CareerQuest.Common.Domain.Abstractions;

namespace CareerQuest.Common.Application.Authorization;

public static class AuthorizationProviderErrors
{
    public static readonly Error UnidentifiedUser = Error.Problem(
        "Authorization.UnidentifiedUser",
        "User was not detected.");
}

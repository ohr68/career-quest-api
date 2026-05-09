using System.Security.Claims;
using CareerQuest.Common.Application.Exceptions;

namespace CareerQuest.Common.Infrastructure.Authentication;

public static class ClaimsPrincipalExtensions
{
    extension(ClaimsPrincipal? principal)
    {
        public Guid GetUserId()
        {
            string? userId = principal?.FindFirst(CustomClaims.Sub)?.Value;

            return Guid.TryParse(userId, out Guid parsedUserId)
                ? parsedUserId
                : throw new CareerQuestException("User identifier is unavailable.");
        }

        public string GetIdentityId()
        {
            return principal?.FindFirst(ClaimTypes.NameIdentifier)?.Value ??
                   throw new CareerQuestException("User identity is unavailable.");
        }

        public HashSet<string> GetPermissions()
        {
            IEnumerable<Claim> permissionClaims = principal?.FindAll(CustomClaims.Permission) ??
                                                  throw new CareerQuestException("Permissions are unavailable.");

            return permissionClaims.Select(c => c.Value).ToHashSet();
        }
    }
}

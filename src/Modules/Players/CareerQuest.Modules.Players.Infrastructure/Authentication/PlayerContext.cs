using CareerQuest.Common.Application.Exceptions;
using CareerQuest.Common.Infrastructure.Authentication;
using CareerQuest.Modules.Players.Application.Abstractions.Authentication;
using Microsoft.AspNetCore.Http;

namespace CareerQuest.Modules.Players.Infrastructure.Authentication;

internal sealed class PlayerContext(IHttpContextAccessor httpContextAccessor) : IPlayerContext
{
    public Guid PlayerId => httpContextAccessor.HttpContext?.User.GetUserId() ??
                            throw new CareerQuestException("User identifier is unavailable.");
}

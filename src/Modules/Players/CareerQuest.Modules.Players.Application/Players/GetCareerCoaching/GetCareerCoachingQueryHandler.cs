using CareerQuest.Common.Application.Caching;
using CareerQuest.Common.Application.Messaging;
using CareerQuest.Common.Domain.Abstractions;
using CareerQuest.Modules.Players.Application.Abstractions.Intelligence;
using CareerQuest.Modules.Players.Domain.Players;

namespace CareerQuest.Modules.Players.Application.Players.GetCareerCoaching;

internal sealed class GetCareerCoachingQueryHandler(
    IPlayerRepository playerRepository,
    ICareerCoach careerCoach,
    ICacheService cacheService)
    : IQueryHandler<GetCareerCoachingQuery, CoachingResponse>
{
    public async Task<Result<CoachingResponse>> Handle(
        GetCareerCoachingQuery request, CancellationToken cancellationToken)
    {
        string cacheKey = $"players:coaching:{request.PlayerId}";

        CoachingResponse? cached = await cacheService.GetAsync<CoachingResponse>(cacheKey, cancellationToken);
        if (cached is not null)
        {
            return cached;
        }

        Player? player = await playerRepository.GetAsync(request.PlayerId, cancellationToken);
        if (player is null)
        {
            return Result.Failure<CoachingResponse>(PlayerErrors.NotFound(request.PlayerId));
        }

        Result<string> advice = await careerCoach.AdviseAsync(BuildContext(player), cancellationToken);
        if (advice.IsFailure)
        {
            return Result.Failure<CoachingResponse>(advice.Error);
        }

        var response = new CoachingResponse(advice.Value);
        await cacheService.SetAsync(cacheKey, response, TimeSpan.FromHours(24), cancellationToken);

        return response;
    }
}


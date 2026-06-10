using CareerQuest.Common.Domain.Abstractions;

namespace CareerQuest.Modules.Players.Application.Abstractions.Intelligence;

public interface ICareerCoach
{
    Task<Result<string>> AdviseAsync(PlayerCoachingContext context, CancellationToken cancellationToken = default);
}

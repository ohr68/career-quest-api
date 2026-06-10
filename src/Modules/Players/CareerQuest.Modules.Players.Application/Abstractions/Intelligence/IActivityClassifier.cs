using CareerQuest.Common.Domain.Abstractions;

namespace CareerQuest.Modules.Players.Application.Abstractions.Intelligence;

public interface IActivityClassifier
{
    Task<Result<ActivityClassification>> ClassifyAsync(string description,
        CancellationToken cancellationToken = default);
}

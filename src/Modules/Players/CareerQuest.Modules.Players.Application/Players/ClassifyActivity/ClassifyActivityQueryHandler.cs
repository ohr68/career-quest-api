using CareerQuest.Common.Application.Messaging;
using CareerQuest.Common.Domain.Abstractions;
using CareerQuest.Modules.Players.Application.Abstractions.Intelligence;

namespace CareerQuest.Modules.Players.Application.Players.ClassifyActivity;

internal sealed class ClassifyActivityQueryHandler(
    IActivityClassifier activityClassifier)
    : IQueryHandler<ClassifyActivityQuery, ActivityClassification>
{
    public async Task<Result<ActivityClassification>> Handle(
        ClassifyActivityQuery request, CancellationToken cancellationToken)
    {
        return await activityClassifier.ClassifyAsync(request.Description, cancellationToken);
    }
}

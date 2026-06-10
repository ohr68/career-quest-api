using CareerQuest.Common.Domain.Abstractions;

namespace CareerQuest.Modules.Players.Application.Abstractions.Intelligence;

public interface IQuestGenerator
{
    Task<Result<IReadOnlyCollection<QuestDraft>>> GenerateAsync(
        PlayerQuestContext context, CancellationToken cancellationToken = default);
}

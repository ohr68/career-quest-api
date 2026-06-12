using CareerQuest.Common.Application.Clock;
using CareerQuest.Common.Application.Messaging;
using CareerQuest.Common.Domain.Abstractions;
using CareerQuest.Modules.Players.Application.Abstractions.Data;
using CareerQuest.Modules.Players.Application.Abstractions.Intelligence;
using CareerQuest.Modules.Players.Domain.Players;

namespace CareerQuest.Modules.Players.Application.Players.GenerateQuests;

internal sealed class GenerateQuestsCommandHandler(
    IPlayerRepository playerRepository,
    IQuestGenerator questGenerator,
    IDateTimeProvider dateTimeProvider,
    IUnitOfWork unitOfWork)
    : ICommandHandler<GenerateQuestsCommand>
{
    private static readonly TimeSpan QuestLifetime = TimeSpan.FromDays(7);

    public async Task<Result> Handle(GenerateQuestsCommand request, CancellationToken cancellationToken)
    {
        Player? player = await playerRepository.GetWithQuestsAsync(request.PlayerId, cancellationToken);

        if (player is null)
        {
            return Result.Failure(PlayerErrors.NotFound(request.PlayerId));
        }

        if (player.Progression is null)
        {
            return Result.Failure(PlayerErrors.ProfileNotCompleted(request.PlayerId));
        }

        var context = new PlayerQuestContext(
            player.CareerStage,
            player.Progression.CurrentLevel,
            player.Classes.Select(c => c.ClassType).ToList(),
            player.Specializations.Select(s => s.SpecializationType).ToList(),
            player.Progression.Transactions
                .OrderByDescending(t => t.EarnedAtUtc)
                .Take(10)
                .Select(t => t.Action)
                .ToList());

        Result<IReadOnlyCollection<QuestDraft>> drafts =
            await questGenerator.GenerateAsync(context, cancellationToken);

        if (drafts.IsFailure)
        {
            return Result.Failure(drafts.Error);
        }

        DateTime utcNow = dateTimeProvider.UtcNow;

        foreach (QuestDraft draft in drafts.Value)
        {
            player.AddQuest(
                draft.Title,
                draft.Description,
                draft.XpReward,
                draft.Difficulty,
                utcNow,
                QuestLifetime);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}

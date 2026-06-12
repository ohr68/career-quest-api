using CareerQuest.Common.Application.Clock;
using CareerQuest.Common.Application.Messaging;
using CareerQuest.Common.Domain.Abstractions;
using CareerQuest.Modules.Players.Application.Abstractions.Data;
using CareerQuest.Modules.Players.Domain.Players;

namespace CareerQuest.Modules.Players.Application.Players.CompleteQuest;

internal sealed class CompleteQuestCommandHandler(
    IPlayerRepository playerRepository,
    IDateTimeProvider dateTimeProvider,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CompleteQuestCommand>
{
    public async Task<Result> Handle(CompleteQuestCommand request, CancellationToken cancellationToken)
    {
        Player? player = await playerRepository.GetWithQuestsAsync(request.PlayerId, cancellationToken);

        if (player is null)
        {
            return Result.Failure(PlayerErrors.NotFound(request.PlayerId));
        }

        Result result = player.CompleteQuest(request.QuestId, dateTimeProvider.UtcNow);

        if (result.IsFailure)
        {
            return result;
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}

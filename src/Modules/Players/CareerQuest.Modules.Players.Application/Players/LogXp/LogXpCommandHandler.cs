using CareerQuest.Common.Application.Clock;
using CareerQuest.Common.Application.Messaging;
using CareerQuest.Common.Domain.Abstractions;
using CareerQuest.Modules.Players.Application.Abstractions.Data;
using CareerQuest.Modules.Players.Domain.Players;

namespace CareerQuest.Modules.Players.Application.Players.LogXp;

internal sealed class LogXpCommandHandler(
    IPlayerRepository playerRepository,
    IDateTimeProvider dateTimeProvider,
    IUnitOfWork unitOfWork) : ICommandHandler<LogXpCommand, LogXpResponse>
{
    public async Task<Result<LogXpResponse>> Handle(LogXpCommand request, CancellationToken cancellationToken)
    {
        Player? player = await playerRepository.GetCurrentProgressAsync(request.PlayerId, cancellationToken);

        if (player is null)
        {
            return Result.Failure<LogXpResponse>(PlayerErrors.NotFound(request.PlayerId));
        }

        XpResult xpResult = player.Progression!.AddXp(
            request.Amount,
            request.Action,
            request.Modifier,
            request.Notes
        );

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new LogXpResponse(
            request.Amount,
            xpResult.FinalAmount,
            xpResult.Multiplier,
            xpResult.CurrentXp,
            xpResult.CurrentLevel,
            xpResult.XpToNextLevel,
            xpResult.LevelsGained > 0,
            xpResult.LevelsGained,
            xpResult.LevelUps,
            xpResult.TotalXp,
            request.Action,
            dateTimeProvider.UtcNow
        );
    }
}

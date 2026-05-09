using CareerQuest.Common.Application.Messaging;
using CareerQuest.Common.Domain.Abstractions;
using CareerQuest.Modules.Players.Application.Abstractions.Data;
using CareerQuest.Modules.Players.Domain.Players;

namespace CareerQuest.Modules.Players.Application.Players.AdvanceCareerStage;

internal sealed class AdvanceCareerStageCommandHandler(
    IPlayerRepository playerRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<AdvanceCareerStageCommand>
{
    public async Task<Result> Handle(
        AdvanceCareerStageCommand request,
        CancellationToken cancellationToken)
    {
        Player? player = await playerRepository.GetAsync(
            request.PlayerId,
            cancellationToken);

        if (player is null)
        {
            return Result.Failure(PlayerErrors.NotFound(request.PlayerId));
        }

        player.AdvanceCareerStage(request.CareerStage);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}

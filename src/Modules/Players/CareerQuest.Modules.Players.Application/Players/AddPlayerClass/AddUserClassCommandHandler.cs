using CareerQuest.Common.Application.Messaging;
using CareerQuest.Common.Domain.Abstractions;
using CareerQuest.Modules.Players.Application.Abstractions.Data;
using CareerQuest.Modules.Players.Domain.Players;

namespace CareerQuest.Modules.Players.Application.Players.AddPlayerClass;

internal sealed class AddUserClassCommandHandler(
    IPlayerRepository playerRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<AddPlayerClassCommand>
{
    public async Task<Result> Handle(
        AddPlayerClassCommand request,
        CancellationToken cancellationToken)
    {
        Player? player = await playerRepository.GetAsync(
            request.PlayerId,
            cancellationToken);

        if (player is null)
        {
            return Result.Failure(PlayerErrors.NotFound(request.PlayerId));
        }

        player.AddClass(request.ClassType);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}

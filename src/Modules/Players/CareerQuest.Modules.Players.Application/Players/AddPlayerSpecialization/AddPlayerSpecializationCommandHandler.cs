using CareerQuest.Common.Application.Messaging;
using CareerQuest.Common.Domain.Abstractions;
using CareerQuest.Modules.Players.Application.Abstractions.Data;
using CareerQuest.Modules.Players.Domain.Players;

namespace CareerQuest.Modules.Players.Application.Players.AddPlayerSpecialization;

internal sealed class AddPlayerSpecializationCommandHandler(
    IPlayerRepository playerRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<AddPlayerSpecializationCommand>
{
    public async Task<Result> Handle(
        AddPlayerSpecializationCommand request,
        CancellationToken cancellationToken)
    {
        Player? player = await playerRepository.GetAsync(
            request.PlayerId,
            cancellationToken);

        if (player is null)
        {
            return Result.Failure(PlayerErrors.NotFound(request.PlayerId));
        }

        player.AddSpecialization(request.SpecializationType);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}

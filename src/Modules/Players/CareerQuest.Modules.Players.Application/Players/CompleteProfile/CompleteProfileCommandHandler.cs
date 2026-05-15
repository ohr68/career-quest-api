using CareerQuest.Common.Application.Messaging;
using CareerQuest.Common.Domain.Abstractions;
using CareerQuest.Modules.Players.Application.Abstractions.Data;
using CareerQuest.Modules.Players.Domain.Players;

namespace CareerQuest.Modules.Players.Application.Players.CompleteProfile;

internal sealed class CompleteProfileCommandHandler(
    IPlayerRepository playerRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CompleteProfileCommand>
{
    public async Task<Result> Handle(CompleteProfileCommand request, CancellationToken cancellationToken)
    {
        Player? player = await playerRepository.GetAsync(request.PlayerId, cancellationToken);

        if (player is null)
        {
            return Result.Failure(PlayerErrors.NotFound(request.PlayerId));
        }

        player.CompleteProfile(
            request.Headline,
            request.AvatarUrl,
            request.CareerStage,
            request.Classes,
            request.Specializations);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}

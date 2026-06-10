using CareerQuest.Common.Application.Clock;
using CareerQuest.Common.Application.Messaging;
using CareerQuest.Common.Domain.Abstractions;
using CareerQuest.Modules.Players.Application.Abstractions.Data;
using CareerQuest.Modules.Players.Domain.Players;

namespace CareerQuest.Modules.Players.Application.Players.CreatePlayer;

internal sealed class CreatePlayerCommandHandler(
    IPlayerRepository playerRepository,
    IDateTimeProvider dateTimeProvider,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreatePlayerCommand>
{
    public async Task<Result> Handle(CreatePlayerCommand request, CancellationToken cancellationToken)
    {
        var player = Player.Create(
            request.PlayerId,
            request.Email,
            string.Join(' ', request.FirstName, request.LastName),
            dateTimeProvider.UtcNow);

        playerRepository.Insert(player);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}

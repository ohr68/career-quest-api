using CareerQuest.Common.Application.Exceptions;
using CareerQuest.Common.Application.Messaging;
using CareerQuest.Common.Domain.Abstractions;
using CareerQuest.Modules.Players.Domain.Players;
using MediatR;

namespace CareerQuest.Modules.Players.Application.Players.GenerateQuests;

internal sealed class PlayerProfileCompletedDomainEventHandler(
    ISender sender)
    : DomainEventHandler<PlayerProfileCompletedDomainEvent>
{
    public override async Task Handle(
        PlayerProfileCompletedDomainEvent notification,
        CancellationToken cancellationToken = default)
    {
        Result result = await sender.Send(
            new GenerateQuestsCommand(notification.PlayerId),
            cancellationToken);

        if (result.IsFailure)
        {
            throw new CareerQuestException(nameof(GenerateQuestsCommand), result.Error);
        }
    }
}

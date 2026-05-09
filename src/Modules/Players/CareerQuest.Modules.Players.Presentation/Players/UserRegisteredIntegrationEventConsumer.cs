using CareerQuest.Common.Application.Exceptions;
using CareerQuest.Common.Domain.Abstractions;
using CareerQuest.Modules.Players.Application.Players.CreatePlayer;
using CareerQuest.Modules.Users.IntegrationEvents;
using MassTransit;
using MediatR;

namespace CareerQuest.Modules.Players.Presentation.Players;

public sealed class UserRegisteredIntegrationEventConsumer(ISender sender)
    : IConsumer<UserRegisteredIntegrationEvent>
{
    public async Task Consume(ConsumeContext<UserRegisteredIntegrationEvent> context)
    {
        Result result = await sender.Send(
            new CreatePlayerCommand(
                context.Message.UserId,
                context.Message.Email,
                context.Message.FirstName,
                context.Message.LastName));

        if (result.IsFailure)
        {
            throw new CareerQuestException(nameof(CreatePlayerCommand), result.Error);
        }
    }
}

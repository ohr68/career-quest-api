using CareerQuest.Common.Application.Exceptions;
using CareerQuest.Common.Application.Messaging;
using CareerQuest.Common.Domain.Abstractions;
using CareerQuest.Modules.Players.Application.Players.LogXp;
using CareerQuest.Modules.Players.Domain.Players;
using MediatR;

namespace CareerQuest.Modules.Players.Application.Players.CompleteQuest;

internal sealed class QuestCompletedDomainEventHandler(
    ISender sender)
    : DomainEventHandler<QuestCompletedDomainEvent>
{
    public override async Task Handle(
        QuestCompletedDomainEvent notification,
        CancellationToken cancellationToken = default)
    {
        // Easy has a 1x multiplier, so the awarded XP equals the quest's reward —
        // quest rewards flow through the same deterministic pipeline as manual logs.
        Result<LogXpResponse> result = await sender.Send(
            new LogXpCommand(
                notification.PlayerId,
                "quest_completed",
                notification.XpReward,
                DifficultyModifier.Easy,
                $"Quest {notification.QuestId} completed"),
            cancellationToken);

        if (result.IsFailure)
        {
            throw new CareerQuestException(nameof(LogXpCommand), result.Error);
        }
    }
}

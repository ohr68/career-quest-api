using CareerQuest.Common.Domain.Abstractions;

namespace CareerQuest.Modules.Players.Domain.Players;

public sealed class PlayerTitleUnlockedDomainEvent(
    Guid playerId,
    TitleType titleType) : DomainEvent
{
    public Guid PlayerId { get; init; } = playerId;
    public TitleType TitleType { get; init; } = titleType;
}

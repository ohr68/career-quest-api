using CareerQuest.Common.Domain.Abstractions;

namespace CareerQuest.Modules.Players.Domain.Players;

public sealed class PlayerProfileUpdatedDomainEvent(
    Guid playerId,
    string displayName,
    string? headLine,
    Uri? avatarUrl,
    CareerStage careerStage) : DomainEvent
{
    public Guid PlayerId { get; init; } = playerId;
    public string DisplayName { get; init; } = displayName;
    public string? HeadLine { get; init; } = headLine;
    public Uri? AvatarUrl { get; init; } = avatarUrl;
    public CareerStage CareerStage { get; init; } = careerStage;
}

using CareerQuest.Common.Domain.Abstractions;

namespace CareerQuest.Modules.Players.Domain.Players;

public sealed class CareerStageAdvancedDomainEvent(
    Guid userId,
    CareerStage careerStage) : DomainEvent
{
    public Guid UserId { get; init; } = userId;
    public CareerStage CareerStage { get; init; } = careerStage;
}

using CareerQuest.Common.Application.Messaging;
using CareerQuest.Modules.Players.Domain.Players;

namespace CareerQuest.Modules.Players.Application.Players.AdvanceCareerStage;

public sealed record AdvanceCareerStageCommand(
    Guid PlayerId,
    CareerStage CareerStage) : ICommand;

using CareerQuest.Common.Application.Messaging;
using CareerQuest.Modules.Players.Domain.Players;

namespace CareerQuest.Modules.Players.Application.Players.AddPlayerClass;

public sealed record AddPlayerClassCommand(
    Guid PlayerId,
    PlayerClassType ClassType) : ICommand;

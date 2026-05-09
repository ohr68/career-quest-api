using CareerQuest.Common.Application.Messaging;
using CareerQuest.Modules.Players.Domain.Players;

namespace CareerQuest.Modules.Players.Application.Players.AddPlayerSpecialization;

public sealed record AddPlayerSpecializationCommand(
    Guid PlayerId,
    PlayerSpecializationType SpecializationType) : ICommand;

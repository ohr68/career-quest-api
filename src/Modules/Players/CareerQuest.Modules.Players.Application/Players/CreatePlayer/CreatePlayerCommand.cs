using CareerQuest.Common.Application.Messaging;

namespace CareerQuest.Modules.Players.Application.Players.CreatePlayer;

public sealed record CreatePlayerCommand(Guid PlayerId, string Email, string FirstName, string LastName)
    : ICommand;

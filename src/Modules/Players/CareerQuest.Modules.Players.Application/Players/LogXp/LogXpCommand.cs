using CareerQuest.Common.Application.Messaging;
using CareerQuest.Modules.Players.Domain.Players;

namespace CareerQuest.Modules.Players.Application.Players.LogXp;

public sealed record LogXpCommand(
    Guid PlayerId,
    string Action,
    int Amount,
    DifficultyModifier Modifier,
    string? Notes = null) : ICommand<LogXpResponse>;

using CareerQuest.Common.Application.Messaging;

namespace CareerQuest.Modules.Players.Application.Players.GenerateQuests;

public sealed record GenerateQuestsCommand(Guid PlayerId) : ICommand;

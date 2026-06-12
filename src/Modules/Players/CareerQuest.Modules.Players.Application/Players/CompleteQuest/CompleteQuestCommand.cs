using CareerQuest.Common.Application.Messaging;

namespace CareerQuest.Modules.Players.Application.Players.CompleteQuest;

public sealed record CompleteQuestCommand(Guid PlayerId, Guid QuestId) : ICommand;

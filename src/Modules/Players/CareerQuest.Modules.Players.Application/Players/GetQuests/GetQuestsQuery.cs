using CareerQuest.Common.Application.Messaging;

namespace CareerQuest.Modules.Players.Application.Players.GetQuests;

public sealed record GetQuestsQuery(Guid PlayerId) : IQuery<IReadOnlyCollection<QuestResponse>>;

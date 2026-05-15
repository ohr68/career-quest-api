using CareerQuest.Common.Application.Messaging;

namespace CareerQuest.Modules.Players.Application.Players.GetPlayer;

public sealed record GetPlayerQuery(Guid PlayerId) : IQuery<PlayerResponse>;

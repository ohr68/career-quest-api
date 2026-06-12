using CareerQuest.Common.Application.Messaging;

namespace CareerQuest.Modules.Players.Application.Players.GetCareerCoaching;

public sealed record GetCareerCoachingQuery(Guid PlayerId) : IQuery<CoachingResponse>;

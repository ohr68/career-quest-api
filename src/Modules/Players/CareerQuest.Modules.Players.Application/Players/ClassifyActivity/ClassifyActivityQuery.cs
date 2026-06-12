using CareerQuest.Common.Application.Messaging;
using CareerQuest.Modules.Players.Application.Abstractions.Intelligence;

namespace CareerQuest.Modules.Players.Application.Players.ClassifyActivity;

public sealed record ClassifyActivityQuery(string Description) : IQuery<ActivityClassification>;

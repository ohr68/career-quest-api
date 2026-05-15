using CareerQuest.Common.Application.Messaging;
using CareerQuest.Modules.Players.Domain.Players;

namespace CareerQuest.Modules.Players.Application.Players.CompleteProfile;

public sealed record CompleteProfileCommand(
    Guid PlayerId,
    string? Headline,
    Uri? AvatarUrl,
    CareerStage CareerStage,
    IReadOnlyCollection<PlayerClassType> Classes,
    IReadOnlyCollection<PlayerSpecializationType> Specializations) : ICommand;

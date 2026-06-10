using CareerQuest.Common.Application.Clock;
using CareerQuest.Common.Application.Messaging;
using CareerQuest.Common.Domain.Abstractions;
using CareerQuest.Modules.Players.Application.Abstractions.Data;
using CareerQuest.Modules.Players.Domain.Players;

namespace CareerQuest.Modules.Players.Application.Players.CompleteProfile;

internal sealed class CompleteProfileCommandHandler(
    IPlayerRepository playerRepository,
    IDateTimeProvider dateTimeProvider,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CompleteProfileCommand>
{
    public async Task<Result> Handle(CompleteProfileCommand request, CancellationToken cancellationToken)
    {
        Player? player = await playerRepository.GetAsync(request.PlayerId, cancellationToken);

        if (player is null)
        {
            return Result.Failure(PlayerErrors.NotFound(request.PlayerId));
        }


        DateTime utcNow = dateTimeProvider.UtcNow;
        var timeZone = TimeZoneInfo.FindSystemTimeZoneById(player.TimeZoneId);
        var activityDate = DateOnly.FromDateTime(TimeZoneInfo.ConvertTimeFromUtc(utcNow, timeZone));

        player.CompleteProfile(
            request.Headline,
            request.AvatarUrl,
            request.CareerStage,
            request.Classes,
            request.Specializations,
            request.TimeZoneId,
            utcNow,
            activityDate);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}

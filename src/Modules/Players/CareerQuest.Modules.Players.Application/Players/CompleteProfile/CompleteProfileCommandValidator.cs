using FluentValidation;

namespace CareerQuest.Modules.Players.Application.Players.CompleteProfile;

internal sealed class CompleteProfileCommandValidator : AbstractValidator<CompleteProfileCommand>
{
    public CompleteProfileCommandValidator()
    {
        RuleFor(c => c.AvatarUrl)
            .NotEmpty();

        RuleFor(c => c.CareerStage)
            .IsInEnum();

        RuleFor(c => c.Classes)
            .NotEmpty();

        RuleFor(c => c.Specializations)
            .NotEmpty();

        RuleForEach(c => c.Classes)
            .IsInEnum();

        RuleForEach(c => c.Specializations)
            .IsInEnum();
    }
}

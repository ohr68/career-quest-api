using FluentValidation;

namespace CareerQuest.Modules.Players.Application.Players.AdvanceCareerStage;

internal sealed class AdvanceCareerStageCommandValidator : AbstractValidator<AdvanceCareerStageCommand>
{
    public AdvanceCareerStageCommandValidator()
    {
        RuleFor(a => a.PlayerId)
            .NotEmpty();

        RuleFor(a => a.CareerStage)
            .IsInEnum();
    }
}

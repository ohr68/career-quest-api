using FluentValidation;

namespace CareerQuest.Modules.Players.Application.Players.ClassifyActivity;

internal sealed class ClassifyActivityQueryValidator : AbstractValidator<ClassifyActivityQuery>
{
    public ClassifyActivityQueryValidator()
    {
        RuleFor(c => c.Description)
            .NotEmpty()
            .MaximumLength(2000);
    }
}

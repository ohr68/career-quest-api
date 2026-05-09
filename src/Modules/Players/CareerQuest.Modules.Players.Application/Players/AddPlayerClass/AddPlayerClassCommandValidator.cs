using FluentValidation;

namespace CareerQuest.Modules.Players.Application.Players.AddPlayerClass;

internal sealed class AddPlayerClassCommandValidator : AbstractValidator<AddPlayerClassCommand>
{
    public AddPlayerClassCommandValidator()
    {
        RuleFor(a => a.PlayerId)
            .NotEmpty();

        RuleFor(a => a.ClassType)
            .NotEmpty();
    }
}

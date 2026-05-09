using FluentValidation;

namespace CareerQuest.Modules.Players.Application.Players.AddPlayerSpecialization;

internal sealed class AddPlayerSpecializationCommandValidator : AbstractValidator<AddPlayerSpecializationCommand>
{
    public AddPlayerSpecializationCommandValidator()
    {
        RuleFor(c => c.PlayerId)
            .NotEmpty();

        RuleFor(c => c.SpecializationType)
            .IsInEnum();
    }
}

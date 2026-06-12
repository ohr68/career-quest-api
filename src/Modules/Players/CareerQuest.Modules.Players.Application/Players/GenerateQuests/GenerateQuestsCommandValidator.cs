using FluentValidation;

namespace CareerQuest.Modules.Players.Application.Players.GenerateQuests;

internal sealed class GenerateQuestsCommandValidator : AbstractValidator<GenerateQuestsCommand>
{
    public GenerateQuestsCommandValidator()
    {
        RuleFor(g => g.PlayerId)
            .NotEmpty();
    }
}

using FluentValidation;

namespace CareerQuest.Modules.Players.Application.Players.LogXp;

internal sealed class LogXpCommandValidator : AbstractValidator<LogXpCommand>
{
    public LogXpCommandValidator()
    {
        RuleFor(l => l.PlayerId)
            .NotEmpty();

        RuleFor(l => l.Amount)
            .NotEmpty();

        RuleFor(l => l.Modifier)
            .IsInEnum();
    }
}

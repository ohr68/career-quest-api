using FluentValidation;

namespace CareerQuest.Modules.Players.Application.Players.CompleteQuest;

internal sealed class CompleteQuestCommandValidator : AbstractValidator<CompleteQuestCommand>
{
    public CompleteQuestCommandValidator()
    {
        RuleFor(c => c.PlayerId)
            .NotEmpty();

        RuleFor(c => c.QuestId)
            .NotEmpty();
    }
}

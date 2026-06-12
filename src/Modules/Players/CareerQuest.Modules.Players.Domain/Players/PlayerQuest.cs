using CareerQuest.Common.Domain.Abstractions;

namespace CareerQuest.Modules.Players.Domain.Players;

public sealed class PlayerQuest : Entity
{
    private PlayerQuest()
    {
    }

    public Guid Id { get; init; }
    public Guid PlayerId { get; private set; }
    public string Title { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public int XpReward { get; private set; }
    public DifficultyModifier Difficulty { get; private set; }
    public DateTime ExpiresAtUtc { get; private set; }
    public DateTime? CompletedAtUtc { get; private set; }

    public static PlayerQuest Create(Guid playerId, string title, string description,
        int xpReward, DifficultyModifier difficulty, DateTime utcNow, TimeSpan lifetime)
    {
        PlayerQuest quest = new()
        {
            Id = Guid.CreateVersion7(),
            PlayerId = playerId,
            Title = title,
            Description = description,
            XpReward = xpReward,
            Difficulty = difficulty,
            ExpiresAtUtc = utcNow.Add(lifetime),
        };

        return quest;
    }

    public void Complete(DateTime utcNow)
    {
        if (CompletedAtUtc is not null)
        {
            return;
        }

        CompletedAtUtc = utcNow;
        Raise(new QuestCompletedDomainEvent(PlayerId, Id, XpReward));
        // a QuestCompletedDomainEventHandler then dispatches LogXpCommand —
        // quest completion feeds the same deterministic XP pipeline as everything else
    }
}

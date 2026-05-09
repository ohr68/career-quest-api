namespace CareerQuest.Modules.Players.Domain.Players;

public sealed class PlayerClass
{
    private PlayerClass()
    {
    }

    public Guid PlayerId { get; init; }

    public PlayerClassType ClassType { get; init; }

    public static PlayerClass Create(Guid playerId, PlayerClassType classType)
    {
        var playerClass = new PlayerClass
        {
            PlayerId = playerId,
            ClassType = classType,
        };

        return playerClass;
    }
}

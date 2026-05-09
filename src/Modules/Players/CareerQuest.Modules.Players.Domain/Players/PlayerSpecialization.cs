namespace CareerQuest.Modules.Players.Domain.Players;

public sealed class PlayerSpecialization
{
    private PlayerSpecialization()
    {
    }

    public Guid PlayerId { get; private set; }

    public PlayerSpecializationType SpecializationType { get; private set; }

    public static PlayerSpecialization Create(
        Guid playerId,
        PlayerSpecializationType specializationType)
    {
        return new PlayerSpecialization
        {
            PlayerId = playerId,
            SpecializationType = specializationType,
        };
    }
}

using CareerQuest.Common.Domain.Abstractions;

namespace CareerQuest.Modules.Players.Domain.Players;

public sealed class Player : Entity
{
    private readonly List<PlayerClass> _classes = [];
    private readonly List<PlayerSpecialization> _specializations = [];
    private readonly List<PlayerTitle> _titles = [];

    private Player()
    {
    }

    public Guid Id { get; init; }

    public string DisplayName { get; private set; }

    public string Email { get; private set; }

    public Uri? AvatarUrl { get; private set; }

    public string? Headline { get; private set; }

    public CareerStage CareerStage { get; private set; }

    public DateTime JoinedAtUtc { get; private set; }

    public DateTime LastActiveAtUtc { get; private set; }

    public PlayerProgression? Progression { get; private set; }

    public PlayerStatistics? Statistics { get; private set; }

    public PlayerStreak? Streak { get; private set; }

    public bool IsProfileCompleted =>
        Progression is not null &&
        Statistics is not null &&
        Streak is not null &&
        _classes.Count > 0 &&
        _specializations.Count > 0;

    public IReadOnlyCollection<PlayerClass> Classes =>
        _classes.ToList();

    public IReadOnlyCollection<PlayerSpecialization> Specializations =>
        _specializations.ToList();

    public IReadOnlyCollection<PlayerTitle> Titles =>
        _titles.ToList();

    public PlayerTitle? CurrentTitle =>
        _titles.SingleOrDefault(x => x.IsCurrent);

    public static Player Create(
        Guid id,
        string email,
        string displayName)
    {
        DateTime utcNow = DateTime.UtcNow;

        var player = new Player
        {
            Id = id,
            Email = email,
            DisplayName = displayName,
            JoinedAtUtc = utcNow,
            LastActiveAtUtc = utcNow,
        };

        player.Raise(new PlayerCreatedDomainEvent(player.Id));

        return player;
    }

    public void UpdateProfile(
        string displayName,
        string? headline,
        Uri? avatarUrl,
        CareerStage careerStage)
    {
        if (DisplayName == displayName &&
            Headline == headline &&
            AvatarUrl == avatarUrl &&
            CareerStage == careerStage)
        {
            return;
        }

        DisplayName = displayName;
        Headline = headline;
        AvatarUrl = avatarUrl;
        CareerStage = careerStage;

        Touch();

        Raise(new PlayerProfileUpdatedDomainEvent(
            Id,
            DisplayName,
            Headline,
            AvatarUrl,
            CareerStage));
    }

    public void CompleteProfile(
        string? headline,
        Uri? avatarUrl,
        CareerStage careerStage,
        IReadOnlyCollection<PlayerClassType> playerClassTypes,
        IReadOnlyCollection<PlayerSpecializationType> playerSpecializationTypes)
    {
        if (Progression is not null)
        {
            return;
        }

        Headline = headline;
        AvatarUrl = avatarUrl;
        CareerStage = careerStage;

        Progression = PlayerProgression.Create(Id);

        Statistics = PlayerStatistics.Create(Id);

        Streak = PlayerStreak.Create(Id);

        _titles.Add(
            PlayerTitle.Create(
                Id,
                TitleType.AnonymousDeveloper,
                true));


        foreach (PlayerClassType playerClassType in playerClassTypes)
        {
            AddClass(playerClassType);
        }

        foreach (PlayerSpecializationType playerSpecializationType in playerSpecializationTypes)
        {
            AddSpecialization(playerSpecializationType);
        }

        Touch();

        Raise(new PlayerProfileCompletedDomainEvent(Id));
    }


    public void AddClass(PlayerClassType classType)
    {
        if (_classes.Any(x => x.ClassType == classType))
        {
            return;
        }

        var playerClass = PlayerClass.Create(
            Id,
            classType);

        _classes.Add(playerClass);

        Touch();

        Raise(new PlayerClassAddedDomainEvent(
            Id,
            classType));
    }

    public void AddSpecialization(
        PlayerSpecializationType specializationType)
    {
        if (_specializations.Any(x => x.SpecializationType == specializationType))
        {
            return;
        }

        var specialization = PlayerSpecialization.Create(
            Id,
            specializationType);

        _specializations.Add(specialization);

        Touch();

        Raise(new PlayerSpecializationAddedDomainEvent(
            Id,
            specializationType));
    }

    public void UnlockTitle(TitleType titleType)
    {
        if (_titles.Any(x => x.TitleType == titleType))
        {
            return;
        }

        bool isFirstTitle = _titles.Count == 0;

        var title = PlayerTitle.Create(
            Id,
            titleType,
            isFirstTitle);

        _titles.Add(title);

        Touch();

        Raise(new PlayerTitleUnlockedDomainEvent(
            Id,
            titleType));
    }

    public void EquipTitle(TitleType titleType)
    {
        PlayerTitle? title = _titles
            .SingleOrDefault(x => x.TitleType == titleType);

        if (title is null)
        {
            return;
        }

        foreach (PlayerTitle playerTitle in _titles.Where(x => x.IsCurrent))
        {
            playerTitle.RemoveAsCurrent();
        }

        title.SetAsCurrent();

        Touch();

        Raise(new PlayerTitleEquippedDomainEvent(
            Id,
            titleType));
    }

    public void AdvanceCareerStage(CareerStage stage)
    {
        if (CareerStage == stage)
        {
            return;
        }

        CareerStage = stage;

        Touch();

        Raise(new CareerStageAdvancedDomainEvent(
            Id,
            CareerStage));
    }

    public void UpdateLastActivity()
    {
        LastActiveAtUtc = DateTime.UtcNow;
    }

    private void Touch()
    {
        LastActiveAtUtc = DateTime.UtcNow;
    }
}

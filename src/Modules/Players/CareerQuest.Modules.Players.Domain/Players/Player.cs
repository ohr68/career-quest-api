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

    public string TimeZoneId { get; private set; } = "UTC";

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
        _classes.AsReadOnly();

    public IReadOnlyCollection<PlayerSpecialization> Specializations =>
        _specializations.AsReadOnly();

    public IReadOnlyCollection<PlayerTitle> Titles =>
        _titles.AsReadOnly();

    public PlayerTitle? CurrentTitle =>
        _titles.SingleOrDefault(x => x.IsCurrent);

    public static Player Create(
        Guid id,
        string email,
        string displayName,
        DateTime utcNow)
    {
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
        CareerStage careerStage,
        DateTime utcNow)
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

        Touch(utcNow);

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
        IReadOnlyCollection<PlayerSpecializationType> playerSpecializationTypes,
        string timeZoneId,
        DateTime utcNow,
        DateOnly activityDate)
    {
        if (Progression is not null && Statistics is not null && Streak is not null)
        {
            return;
        }

        Headline = headline;
        AvatarUrl = avatarUrl;
        CareerStage = careerStage;
        TimeZoneId = timeZoneId;

        Progression = PlayerProgression.Create(Id);

        Statistics = PlayerStatistics.Create(Id);

        Streak = PlayerStreak.Create(Id);
        Streak.RegisterActivity(activityDate);

        PlayerTitle? existing = _titles.FirstOrDefault(x => x.TitleType == TitleType.AnonymousDeveloper);

        if (existing is null)
        {
            _titles.Add(
                PlayerTitle.Create(
                    Id,
                    TitleType.AnonymousDeveloper,
                    utcNow,
                    true));
        }

        foreach (PlayerClassType playerClassType in playerClassTypes)
        {
            AddClass(playerClassType, utcNow);
        }

        foreach (PlayerSpecializationType playerSpecializationType in playerSpecializationTypes)
        {
            AddSpecialization(playerSpecializationType, utcNow);
        }

        Touch(utcNow);

        Raise(new PlayerProfileCompletedDomainEvent(Id));
    }


    public void AddClass(PlayerClassType classType, DateTime utcNow)
    {
        if (_classes.Any(x => x.ClassType == classType))
        {
            return;
        }

        var playerClass = PlayerClass.Create(
            Id,
            classType);

        _classes.Add(playerClass);

        Touch(utcNow);

        Raise(new PlayerClassAddedDomainEvent(
            Id,
            classType));
    }

    public void AddSpecialization(
        PlayerSpecializationType specializationType,
        DateTime utcNow)
    {
        if (_specializations.Any(x => x.SpecializationType == specializationType))
        {
            return;
        }

        var specialization = PlayerSpecialization.Create(
            Id,
            specializationType);

        _specializations.Add(specialization);

        Touch(utcNow);

        Raise(new PlayerSpecializationAddedDomainEvent(
            Id,
            specializationType));
    }

    public void UnlockTitle(TitleType titleType, DateTime utcNow)
    {
        PlayerTitle? existing = _titles.FirstOrDefault(x => x.TitleType == titleType);

        if (existing is not null)
        {
            return;
        }

        bool isFirstTitle = _titles.Count == 0;

        var title = PlayerTitle.Create(
            Id,
            titleType,
            utcNow,
            isFirstTitle);

        _titles.Add(title);

        Touch(utcNow);

        Raise(new PlayerTitleUnlockedDomainEvent(
            Id,
            titleType));
    }

    public void EquipTitle(TitleType titleType, DateTime utcNow)
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

        Touch(utcNow);

        Raise(new PlayerTitleEquippedDomainEvent(
            Id,
            titleType));
    }

    public void AdvanceCareerStage(CareerStage stage, DateTime utcNow)
    {
        if (CareerStage == stage)
        {
            return;
        }

        CareerStage = stage;

        Touch(utcNow);

        Raise(new CareerStageAdvancedDomainEvent(
            Id,
            CareerStage));
    }

    public void RegisterActivity(DateOnly activityDate, DateTime utcNow)
    {
        if (Streak is null)
        {
            return;
        }

        int previousDays = Streak.CurrentDays;

        Streak.RegisterActivity(activityDate);

        Touch(utcNow);

        if (Streak.CurrentDays != previousDays)
        {
            Raise(new PlayerStreakUpdatedDomainEvent(
                Id,
                Streak.CurrentDays,
                Streak.LongestDays,
                Streak.CurrentMultiplier)
            );
        }
    }

    public void UpdateLastActivity(DateTime utcNow)
    {
        LastActiveAtUtc = utcNow;
    }

    private void Touch(DateTime utcNow)
    {
        LastActiveAtUtc = utcNow;
    }
}

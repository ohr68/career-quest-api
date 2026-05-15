using System.Text.Json.Serialization;

namespace CareerQuest.Modules.Players.Domain.Players;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum CareerStage
{
    Student = 1,
    Junior = 2,
    MidLevel = 3,
    Senior = 4,
    Lead = 5,
    Principal = 6,
    Freelancer = 7,
    Consultant = 8,
}

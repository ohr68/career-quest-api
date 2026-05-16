using System.Text.Json.Serialization;

namespace CareerQuest.Modules.Players.Domain.Players;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum DifficultyModifier
{
    Easy,
    Medium,
    High,
}

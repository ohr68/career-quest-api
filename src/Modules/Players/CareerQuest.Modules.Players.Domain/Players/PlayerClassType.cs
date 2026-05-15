using System.Text.Json.Serialization;

namespace CareerQuest.Modules.Players.Domain.Players;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum PlayerClassType
{
    SystemsEngineer = 1,
    FullstackIntegrator = 2,
    ArchitectureConsultant = 3,
}

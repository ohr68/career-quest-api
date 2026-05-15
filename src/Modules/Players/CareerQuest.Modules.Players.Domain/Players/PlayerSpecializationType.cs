using System.Text.Json.Serialization;

namespace CareerQuest.Modules.Players.Domain.Players;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum PlayerSpecializationType
{
    DistributedSystems = 1,
    EventDrivenArchitecture = 2,
    Observability = 3,
    RealTimeCommunication = 4,
    AspNetCore = 5,
    React = 6,
    TypeScript = 7,
    SignalR = 8,
    Docker = 9,
    OpenTelemetry = 10,
    PostgresSql = 11,
    Redis = 12,
    SystemDesign = 13,
    IntegrationArchitecture = 14,
    PerformanceOptimization = 15,
    ResiliencePatterns = 16,
    BackendScalability = 17,
    FrontendIntegration = 18,
}

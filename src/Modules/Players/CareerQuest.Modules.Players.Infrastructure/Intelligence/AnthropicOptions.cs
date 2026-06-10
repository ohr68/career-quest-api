namespace CareerQuest.Modules.Players.Infrastructure.Intelligence;

public sealed class AnthropicOptions
{
    public string ApiKey { get; init; } = string.Empty;
    public string ClassifierModel { get; init; } = "claude-haiku-4-5";
    public string GenerationModel { get; init; } = "claude-opus-4-8";
}

namespace CareerQuest.Modules.Users.Infrastructure.Identity;

internal sealed class KeyCloakOptions
{
    public string AdminUrl { get; set; } = null!;
    public string TokenUrl { get; set; } = null!;
    public string ConfidentialClientId { get; set; } = null!;
    public string ConfidentialClientSecret { get; set; } = null!;
    public string PublicClientId { get; set; } = null!;
}
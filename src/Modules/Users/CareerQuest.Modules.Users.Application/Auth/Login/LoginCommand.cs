using CareerQuest.Common.Application.Messaging;
using CareerQuest.Modules.Users.Application.Abstractions.Identity;

namespace CareerQuest.Modules.Users.Application.Auth.Login;

public sealed record LoginCommand(string Email, string Password) : ICommand<AuthResponse>;

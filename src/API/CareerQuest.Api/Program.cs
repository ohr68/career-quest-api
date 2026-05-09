using System.Reflection;
using CareerQuest.Api.Extensions;
using CareerQuest.Api.Middleware;
using CareerQuest.Common.Application;
using CareerQuest.Common.Infrastructure;
using CareerQuest.Common.Infrastructure.Configuration;
using CareerQuest.Common.Presentation.Endpoints;
using CareerQuest.Modules.Players.Infrastructure;
using CareerQuest.Modules.Users.Application;
using CareerQuest.Modules.Users.Infrastructure;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfig) => loggerConfig.ReadFrom.Configuration(context.Configuration));

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerDocumentation();

builder.Services.AddCors(options =>
{
    options.AddPolicy("DefaultCors", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

Assembly[] moduleApplicationAssemblies =
[
    AssemblyReference.Assembly,
    CareerQuest.Modules.Players.Application.AssemblyReference.Assembly,
];

builder.Services.AddApplication(moduleApplicationAssemblies);

string databaseConnectionString = builder.Configuration.GetConnectionStringOrThrow("Database");
string redisConnectionString = builder.Configuration.GetConnectionStringOrThrow("Cache");

builder.Services.AddInfrastructure(
    [
        PlayersModule.ConfigureConsumers,
    ],
    databaseConnectionString,
    redisConnectionString);

builder.Configuration.AddModuleConfiguration(["users", "players"]);

Uri keyCloakHealthUrl = builder.Configuration.GetKeyCloakHealthUrl();

builder.Services.AddHealthChecks()
    .AddNpgSql(databaseConnectionString)
    .AddRedis(redisConnectionString)
    .AddKeyCloak(keyCloakHealthUrl);

builder.Services.AddUsersModule(builder.Configuration);
builder.Services.AddPlayersModule(builder.Configuration);

builder.Host.UseWindowsService();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.ApplyMigrations();
}

app.MapHealthChecks("health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
});

app.UseSerilogRequestLogging();

app.UseExceptionHandler();

app.UseCors("DefaultCors");

app.UseAuthentication();

app.UseAuthorization();

app.MapEndpoints();

await app.RunAsync();

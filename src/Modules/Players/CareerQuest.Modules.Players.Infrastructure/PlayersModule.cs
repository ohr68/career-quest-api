using CareerQuest.Common.Application.EventBus;
using CareerQuest.Common.Application.Messaging;
using CareerQuest.Common.Infrastructure.Configuration;
using CareerQuest.Common.Infrastructure.Outbox;
using CareerQuest.Common.Presentation.Endpoints;
using CareerQuest.Modules.Players.Application.Abstractions.Authentication;
using CareerQuest.Modules.Players.Application.Abstractions.Data;
using CareerQuest.Modules.Players.Domain.Players;
using CareerQuest.Modules.Players.Infrastructure.Authentication;
using CareerQuest.Modules.Players.Infrastructure.Database;
using CareerQuest.Modules.Players.Infrastructure.Inbox;
using CareerQuest.Modules.Players.Infrastructure.Outbox;
using CareerQuest.Modules.Players.Infrastructure.Players;
using CareerQuest.Modules.Players.Presentation;
using CareerQuest.Modules.Players.Presentation.Players;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CareerQuest.Modules.Players.Infrastructure;

public static class PlayersModule
{
    public static void ConfigureConsumers(IRegistrationConfigurator registrationConfigurator)
    {
        registrationConfigurator.AddConsumer<UserRegisteredIntegrationEventConsumer>();
    }

    extension(IServiceCollection services)
    {
        public void AddPlayersModule(IConfiguration configuration)
        {
            services.AddDomainEventHandlers();

            services.AddIntegrationEventHandlers();

            services.AddInfrastructure(configuration);

            services.AddEndpoints([AssemblyReference.Assembly]);
        }

        private void AddInfrastructure(IConfiguration configuration)
        {
            services.AddDbContext<PlayersDbContext>((sp, options) =>
                options.UseNpgsql(configuration.GetConnectionStringOrThrow("Database"),
                        npgsqlOptions => npgsqlOptions
                            .MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Players))
                    .AddInterceptors(sp.GetRequiredService<InsertOutboxMessagesInterceptor>())
                    .UseSnakeCaseNamingConvention());


            services.AddTransient<IPlayerContext, PlayerContext>();

            services.AddScoped<IPlayerRepository, PlayerRepository>();

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<PlayersDbContext>());

            services.Configure<OutboxOptions>(configuration.GetRequiredSection("Players:Outbox"));

            services.ConfigureOptions<ConfigureProcessOutboxJob>();

            services.Configure<InboxOptions>(configuration.GetRequiredSection("Players:Inbox"));

            services.ConfigureOptions<ConfigureProcessInboxJob>();
        }

        private void AddDomainEventHandlers()
        {
            Type[] domainEventHandlers = Application.AssemblyReference.Assembly
                .GetTypes()
                .Where(t => t.IsAssignableTo(typeof(IDomainEventHandler)))
                .ToArray();

            foreach (Type domainEventHandler in domainEventHandlers)
            {
                services.TryAddScoped(domainEventHandler);

                Type domainEvent = domainEventHandler
                    .GetInterfaces()
                    .Single(i => i.IsGenericType)
                    .GetGenericArguments()
                    .Single();

                Type closedIdempotentHandler = typeof(IdempotentDomainEventHandler<>).MakeGenericType(domainEvent);

                services.Decorate(domainEventHandler, closedIdempotentHandler);
            }
        }


        private void AddIntegrationEventHandlers()
        {
            Type[] integrationEventHandlers = AssemblyReference.Assembly
                .GetTypes()
                .Where(t => t.IsAssignableTo(typeof(IIntegrationEventHandler)))
                .ToArray();

            foreach (Type integrationEventHandler in integrationEventHandlers)
            {
                services.TryAddScoped(integrationEventHandler);

                Type integrationEvent = integrationEventHandler
                    .GetInterfaces()
                    .Single(i => i.IsGenericType)
                    .GetGenericArguments()
                    .Single();

                Type closedIdempotentHandler =
                    typeof(IdempotentIntegrationEventHandler<>).MakeGenericType(integrationEvent);

                services.Decorate(integrationEventHandler, closedIdempotentHandler);
            }
        }
    }
}

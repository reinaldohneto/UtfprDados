using System.Collections.Immutable;
using MassTransit;

namespace Utfpr.Dados.API.Configurations;

public static class MessageQueueConfiguration
{
    public static void ConfigureMessageQueue(this IServiceCollection services)
    {
        var connectionString = Environment.GetEnvironmentVariable("RABBITMQ_CONNECTION_STRING");
        services.AddMassTransit(bus =>
        {
            bus.UsingRabbitMq((ctx, busConfigurator) =>
            {
                busConfigurator.Host(connectionString);
            });
        });
    }
}
using MassTransit;
using Utfpr.Dados.Worker.Application.MessageHandlers;
using Utfpr.Dados.Worker.Configuration;

var configuration = new ConfigurationBuilder()
    .AddEnvironmentVariables()
    .AddCommandLine(args)
    .AddJsonFile("appsettings.json")
    .Build();

await Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(builder =>
    {
        builder.Sources.Clear();
        builder.AddConfiguration(configuration);
    })
    .ConfigureServices(config =>
    {
        config.AddMassTransit(x =>
        {
            x.AddConsumer<SolicitacaoProcessamentoMessageHandler>(typeof(SolicitacaoProcessamentoMessageDefinition));
            x.SetKebabCaseEndpointNameFormatter();
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.ConfigureEndpoints(context);
                cfg.Host(Environment.GetEnvironmentVariable("RABBITMQ_CONNECTION_STRING"));
            });
        });
        config.ConfigureDependencyInjection();
        config.ConfigureDatabase(Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT"));
    })
    .Build()
    .RunAsync();
using MassTransit;

namespace Utfpr.Dados.Worker.Application.MessageHandlers;

public class SolicitacaoProcessamentoMessageDefinition : ConsumerDefinition<SolicitacaoProcessamentoMessageHandler>
{
    public SolicitacaoProcessamentoMessageDefinition()
    {
        EndpointName = "solicitacoes-processamento";
        ConcurrentMessageLimit = 8;
    }
    
    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
        IConsumerConfigurator<SolicitacaoProcessamentoMessageHandler> consumerConfigurator)
    {
        endpointConfigurator.UseMessageRetry(r => r.Intervals(100,200,500,800,1000));
    }
}
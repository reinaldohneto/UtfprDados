using Utfpr.Dados.Worker.Application.Notification;
using Utfpr.Dados.Worker.Domain;
using Utfpr.Dados.Worker.Domain.SolicitacoesProcessamento.Entities;
using Utfpr.Dados.Worker.Domain.SolicitacoesProcessamento.Interfaces;

namespace Utfpr.Dados.Worker.Infrastructure.Data.Repositories;

public class SolicitacaoProcessamentoRepository : Repository<SolicitacaoProcessamento>, ISolicitacaoProcessamentoRepository
{
    public SolicitacaoProcessamentoRepository(ApplicationContext context, NotificationContext notificationContext) : base(context, notificationContext)
    {
    }
    
    
}
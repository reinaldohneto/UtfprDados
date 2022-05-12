using Utfpr.Dados.API.Application.Notification;
using Utfpr.Dados.API.Domain;
using Utfpr.Dados.API.Domain.SolicitacoesProcessamento.Entities;
using Utfpr.Dados.API.Domain.SolicitacoesProcessamento.Interfaces;

namespace Utfpr.Dados.API.Data.Repositories;

public class SolicitacaoProcessamentoRepository : Repository<SolicitacaoProcessamento>, ISolicitacaoProcessamentoRepository
{
    public SolicitacaoProcessamentoRepository(ApplicationContext context, NotificationContext notificationContext) : base(context, notificationContext)
    {
    }
    
    
}
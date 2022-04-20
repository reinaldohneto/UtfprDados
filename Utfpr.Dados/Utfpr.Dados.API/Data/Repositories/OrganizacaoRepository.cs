using Utfpr.Dados.API.Application.Notification;
using Utfpr.Dados.API.Domain;
using Utfpr.Dados.API.Domain.Organizacoes.Entities;
using Utfpr.Dados.API.Domain.Organizacoes.Interfaces;

namespace Utfpr.Dados.API.Data.Repositories;

public class OrganizacaoRepository : Repository<Organizacao>, IOrganizacaoRepository
{
    public OrganizacaoRepository(ApplicationContext context, NotificationContext notificationContext) : base(context, notificationContext)
    {
    }
}
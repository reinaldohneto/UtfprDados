using Microsoft.EntityFrameworkCore;
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

    public async Task<bool> ExisteSolicitacaoVinculada(Guid id)
    {
        var registro = await DbSet
            .Include(f => f.SolicitacoesProcessamento)
            .FirstOrDefaultAsync(f => f.Id.Equals(id));

        if (registro == null)
        {
            NotificationContext.NotFound(nameof(Mensagens.RegistroNaoEncontrado), 
                string.Format(Mensagens.RegistroNaoEncontrado, "OrganizacaoId"));
            return true;
        }

        return registro.SolicitacoesProcessamento.Any();
    }
}
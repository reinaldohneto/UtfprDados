using Utfpr.Dados.Worker.Domain.Organizacoes.Entities;

namespace Utfpr.Dados.Worker.Domain.Organizacoes.Interfaces;

public interface IOrganizacaoRepository : IRepository<Organizacao>
{
    Task<bool> ExisteSolicitacaoVinculada(Guid id);
}
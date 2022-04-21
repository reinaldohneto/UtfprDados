using Utfpr.Dados.API.Application.Organizacao.ViewModels;

namespace Utfpr.Dados.API.Application.Organizacao.Queries;

public class ObterOrganizacoesQuery : QueryPaginada<ResultadoPaginadoViewModel<OrganizacaoViewModel>>
{
    public ObterOrganizacoesQuery(int pagina, int itensPorPagina) : base(pagina, itensPorPagina)
    {
    }
}
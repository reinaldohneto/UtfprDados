using Utfpr.Dados.API.Application.Organizacao.ViewModels;

namespace Utfpr.Dados.API.Application.Organizacao.Queries;

public class  ObterOrganizacaoPorIdQuery : Query<OrganizacaoViewModel>
{
    public Guid Id { get; set; }

    public  ObterOrganizacaoPorIdQuery(Guid id)
    {
        Id = id;
    }
}
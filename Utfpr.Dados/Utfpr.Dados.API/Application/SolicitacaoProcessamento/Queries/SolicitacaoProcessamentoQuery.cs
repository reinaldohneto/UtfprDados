using Utfpr.Dados.API.Application.SolicitacaoProcessamento.ViewModels;

namespace Utfpr.Dados.API.Application.SolicitacaoProcessamento.Queries;

public class SolicitacaoProcessamentoQuery : Query<SolicitacaoProcessamentoViewModel>
{
    public Guid Id { get; set; }

    public SolicitacaoProcessamentoQuery(Guid id)
    {
        Id = id;
    }
}
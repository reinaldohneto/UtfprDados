using Utfpr.Dados.API.Application.Organizacao.ViewModels;
using Utfpr.Dados.API.Domain.SolicitacoesProcessamento.Enums;

namespace Utfpr.Dados.API.Application.SolicitacaoProcessamento.ViewModels;

public class SolicitacaoProcessamentoViewModel : BaseViewModel
{
    public string ConjuntoDadosNome { get; set; }
    public Uri ConjuntoDadosLink { get; set; }
    public Uri? LinkPrivadoBucket { get; set; }
    public SolicitacaoProcessamentoStatus ProcessamentoStatus { get; set; }
    public Guid OrganizacaoId { get; set; }
}
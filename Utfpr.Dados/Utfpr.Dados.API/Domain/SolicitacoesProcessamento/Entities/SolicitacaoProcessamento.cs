using FluentValidation;
using Utfpr.Dados.API.Domain.Organizacoes.Entities;
using Utfpr.Dados.API.Domain.SolicitacoesProcessamento.Enums;
using Utfpr.Dados.API.Domain.SolicitacoesProcessamento.Validators;

namespace Utfpr.Dados.API.Domain.SolicitacoesProcessamento.Entities;

public class SolicitacaoProcessamento : ValidatableEntity<SolicitacaoProcessamento>
{
    public string ConjuntoDadosNome { get; set; }
    public Uri ConjuntoDadosLink { get; set; }
    public Uri? LinkPrivadoBucket { get; set; } = null;
    public SolicitacaoProcessamentoStatus ProcessamentoStatus { get; set; } = SolicitacaoProcessamentoStatus.PENDENTE;
    public Guid OrganizacaoId { get; set; }
    public virtual Organizacao Organizacao { get; set; }

    protected override AbstractValidator<SolicitacaoProcessamento> ObterValidator()
    {
        return new SolicitacaoProcessamentoValidator();
    }
}
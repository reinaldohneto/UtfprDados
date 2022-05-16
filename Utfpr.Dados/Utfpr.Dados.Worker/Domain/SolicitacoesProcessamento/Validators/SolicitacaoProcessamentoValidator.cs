using FluentValidation;
using Utfpr.Dados.Worker.Domain.SolicitacoesProcessamento.Entities;

namespace Utfpr.Dados.Worker.Domain.SolicitacoesProcessamento.Validators;

public class SolicitacaoProcessamentoValidator : AbstractValidator<SolicitacaoProcessamento>
{
    public SolicitacaoProcessamentoValidator()
    {
        RuleFor(s => s.ConjuntoDadosNome)
            .Must(string.IsNullOrWhiteSpace)
            .WithMessage(Mensagens.CampoObrigatorio)
            .WithErrorCode(nameof(Mensagens.CampoObrigatorio));

        RuleFor(s => s.ConjuntoDadosLink)
            .NotNull()
            .WithMessage(Mensagens.CampoObrigatorio)
            .WithErrorCode(nameof(Mensagens.CampoObrigatorio));
    }
}
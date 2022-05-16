using FluentValidation;
using Utfpr.Dados.Worker.Domain.Organizacoes.Entities;

namespace Utfpr.Dados.Worker.Domain.Organizacoes.Validators;

public class OrganizacaoValidator : AbstractValidator<Organizacao>
{
    public OrganizacaoValidator()
    {
        RuleFor(o => o.Nome)
            .NotNull()
            .NotEmpty()
            .WithMessage(string.Format(Mensagens.CampoObrigatorio, "Nome"));
    }
}
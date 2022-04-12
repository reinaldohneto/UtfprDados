using FluentValidation;
using Utfpr.Dados.API.Domain.Organizacoes.Entities;

namespace Utfpr.Dados.API.Domain.Organizacoes.Validators;

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
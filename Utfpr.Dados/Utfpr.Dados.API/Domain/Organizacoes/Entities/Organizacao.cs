using FluentValidation;
using Utfpr.Dados.API.Domain.Organizacoes.Validators;
using Utfpr.Dados.API.Domain.Usuarios.Entities;

namespace Utfpr.Dados.API.Domain.Organizacoes.Entities;

public class Organizacao : ValidatableEntity<Organizacao>
{
    public string Nome { get; set; }
    public string? Descricao { get; set; }

    public ICollection<Usuario> Usuarios { get; set; }
    
    protected override AbstractValidator<Organizacao> ObterValidator()
    {
        return new OrganizacaoValidator();
    }
}
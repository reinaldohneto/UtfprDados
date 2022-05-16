using FluentValidation;
using Utfpr.Dados.Worker.Domain.Organizacoes.Validators;
using Utfpr.Dados.Worker.Domain.SolicitacoesProcessamento.Entities;
using Utfpr.Dados.Worker.Domain.Usuarios.Entities;

namespace Utfpr.Dados.Worker.Domain.Organizacoes.Entities;

public class Organizacao : ValidatableEntity<Organizacao>
{
    public string Nome { get; set; }
    public string? Descricao { get; set; }

    public virtual ICollection<Usuario> Usuarios { get; set; }
    public virtual ICollection<SolicitacaoProcessamento> SolicitacoesProcessamento { get; set; }

    protected override AbstractValidator<Organizacao> ObterValidator()
    {
        return new OrganizacaoValidator();
    }

    public Organizacao(Guid id, string nome, string? descricao)
    {
        Id = id;
        Nome = nome;
        Descricao = descricao;
    }
}
using System.ComponentModel.DataAnnotations.Schema;
using Utfpr.Dados.API.Application.Organizacao.ViewModels;

namespace Utfpr.Dados.API.Application.Organizacao.Commands;

public class AtualizarOrganizacaoCommand : Command<OrganizacaoViewModel>
{
    [NotMapped]
    public Guid Id { get; private set; }

    public string Nome { get; set; }
    public string? Descricao { get; set; }

    public AtualizarOrganizacaoCommand AtribuirOrganizacaoId(Guid id)
    {
        Id = id;
        return this;
    }
}
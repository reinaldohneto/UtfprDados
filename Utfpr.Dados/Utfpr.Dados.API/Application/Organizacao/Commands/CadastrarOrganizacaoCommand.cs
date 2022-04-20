using System.ComponentModel.DataAnnotations.Schema;
using Utfpr.Dados.API.Application.Organizacao.ViewModels;

namespace Utfpr.Dados.API.Application.Organizacao.Commands;

public class CadastrarOrganizacaoCommand : Command<OrganizacaoViewModel>
{
    [NotMapped]
    public Guid Id { get; private set; }

    public string Nome { get; set; }
    public string? Descricao { get; set; }

    public CadastrarOrganizacaoCommand()
    {
        Id = Guid.NewGuid();
    }
}
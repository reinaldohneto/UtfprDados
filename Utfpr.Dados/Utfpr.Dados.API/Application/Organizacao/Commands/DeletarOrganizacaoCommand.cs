using System.ComponentModel.DataAnnotations.Schema;

namespace Utfpr.Dados.API.Application.Organizacao.Commands;

public class DeletarOrganizacaoCommand : Command
{
    [NotMapped]
    public Guid Id { get; private set; }

    public DeletarOrganizacaoCommand(Guid id)
    {
        Id = id;
    }
}
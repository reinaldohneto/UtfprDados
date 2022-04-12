using System.Text.Json.Serialization;
using Utfpr.Dados.API.Application.Usuarios.ViewModels;

namespace Utfpr.Dados.API.Application.Usuarios.Commands;

public class CadastrarUsuarioCommand : Command<UsuarioViewModel>
{
    public CadastrarUsuarioCommand()
    {
        Id = Guid.NewGuid();
    }
    [JsonIgnore]
    public Guid Id { get; private set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public Guid OrganizacaoId { get; set; }
}
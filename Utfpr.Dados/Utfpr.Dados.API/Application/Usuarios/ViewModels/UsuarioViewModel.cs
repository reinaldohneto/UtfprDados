namespace Utfpr.Dados.API.Application.Usuarios.ViewModels;

public class UsuarioViewModel : BaseViewModel
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public Guid OrganizacaoId { get; set; }
}
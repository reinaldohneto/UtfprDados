using MediatR;
using Utfpr.Dados.API.Application.Usuarios.ViewModels;

namespace Utfpr.Dados.API.Application.Usuarios.Commands;

public class EfetuarLoginCommand : Command<TokenViewModel>
{
    public string Username { get; set; }
    public string Password { get; set; }
}
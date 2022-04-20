using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utfpr.Dados.API.Application;
using Utfpr.Dados.API.Application.Notification;
using Utfpr.Dados.API.Application.Usuarios.Commands;
using Utfpr.Dados.API.Application.Usuarios.ViewModels;

namespace Utfpr.Dados.API.Controllers;

[Route("api/[controller]")]
[AllowAnonymous]
public class UsuariosController : MainController
{
    public UsuariosController(IMediator mediator, NotificationContext notificacaoContext) : base(mediator, notificacaoContext)
    {
    }

    [HttpPost("cadastrar-usuario")]
    public async Task<ActionResult<UsuarioViewModel>> CadastrarUsuario(CadastrarUsuarioCommand command)
        => await ExecutarCommandCadastro(command, nameof(CadastrarUsuario));

    [HttpPost("login")]
    public async Task<ActionResult<TokenViewModel>> EfetuarLogin(EfetuarLoginCommand command)
        => await ExecutarCommandCadastro(command, nameof(EfetuarLogin));
}
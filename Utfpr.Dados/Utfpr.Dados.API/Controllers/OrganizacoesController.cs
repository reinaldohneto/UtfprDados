using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utfpr.Dados.API.Application.Notification;
using Utfpr.Dados.API.Application.Organizacao.Commands;
using Utfpr.Dados.API.Application.Organizacao.ViewModels;

namespace Utfpr.Dados.API.Controllers;

[Route("api/[controller]")]
[AllowAnonymous]
public class OrganizacoesController : MainController
{
    public OrganizacoesController(IMediator mediator, NotificationContext notificacaoContext) : base(mediator, notificacaoContext)
    {
    }

    [HttpPost]
    public async Task<ActionResult<OrganizacaoViewModel>> CadastrarOrganizacao(CadastrarOrganizacaoCommand command)
        => await ExecutarCommandCadastro(command, nameof(CadastrarOrganizacao));
}
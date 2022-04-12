using MediatR;
using Microsoft.AspNetCore.Mvc;
using Utfpr.Dados.API.Application.Notification;

namespace Utfpr.Dados.API.Controllers;

[Route("api/[controller]")]
public class OrganizacoesController : MainController
{
    public OrganizacoesController(IMediator mediator, NotificationContext notificacaoContext) : base(mediator, notificacaoContext)
    {
    }
}
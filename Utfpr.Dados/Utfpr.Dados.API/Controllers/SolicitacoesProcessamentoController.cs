using MediatR;
using Microsoft.AspNetCore.Mvc;
using Utfpr.Dados.API.Application.Notification;
using Utfpr.Dados.API.Application.SolicitacaoProcessamento.Commands;
using Utfpr.Dados.API.Application.SolicitacaoProcessamento.Queries;
using Utfpr.Dados.API.Application.SolicitacaoProcessamento.ViewModels;

namespace Utfpr.Dados.API.Controllers;

[Route("api/[controller]")]
public class SolicitacoesProcessamentoController : MainController
{
    public SolicitacoesProcessamentoController(IMediator mediator, NotificationContext notificacaoContext) : base(mediator, notificacaoContext)
    {
    }
    
    [HttpPost]
    public async Task<ActionResult<SolicitacaoProcessamentoViewModel>> CadastrarSolicitacaoProcessamento(CadastrarSolicitacaoProcessamentoCommand command)
        => await ExecutarCommandCadastro(command, nameof(CadastrarSolicitacaoProcessamento));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<SolicitacaoProcessamentoViewModel>> ObterSolicitacaoProcessamentoById(Guid id)
        => await ExecutarQuery(new SolicitacaoProcessamentoQuery(id));

}
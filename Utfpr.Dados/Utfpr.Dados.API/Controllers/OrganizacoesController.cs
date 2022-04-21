using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utfpr.Dados.API.Application;
using Utfpr.Dados.API.Application.Notification;
using Utfpr.Dados.API.Application.Organizacao.Commands;
using Utfpr.Dados.API.Application.Organizacao.Queries;
using Utfpr.Dados.API.Application.Organizacao.ViewModels;

namespace Utfpr.Dados.API.Controllers;

[Route("api/[controller]")]
public class OrganizacoesController : MainController
{
    public OrganizacoesController(IMediator mediator, NotificationContext notificacaoContext) : base(mediator, notificacaoContext)
    {
    }

    [HttpPost]
    public async Task<ActionResult<OrganizacaoViewModel>> CadastrarOrganizacao(CadastrarOrganizacaoCommand command)
        => await ExecutarCommandCadastro(command, nameof(CadastrarOrganizacao));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<OrganizacaoViewModel>> ObterOrganizacao(Guid id)
        => await ExecutarQuery(new ObterOrganizacaoPorIdQuery(id));
    
    [HttpGet]
    public async Task<ActionResult<ResultadoPaginadoViewModel<OrganizacaoViewModel>>> ObterOrganizacao(
        [FromQuery] int pagina = PAGINA_PADRAO,
        [FromQuery] int itensPorPagina = ITENS_POR_PAGINA_PADRAO)
        => await ExecutarQueryPaginada(new ObterOrganizacoesQuery(pagina, itensPorPagina));
}
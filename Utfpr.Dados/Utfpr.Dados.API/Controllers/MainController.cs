using MediatR;
using Microsoft.AspNetCore.Mvc;
using Utfpr.Dados.API.Application;
using Utfpr.Dados.API.Application.Notification;

namespace Utfpr.Dados.API.Controllers;

[ApiController]
public abstract class MainController : ControllerBase
{
    protected readonly NotificationContext _notificacaoContext;
    protected readonly IMediator _mediator;

    public const int ITENS_POR_PAGINA_PADRAO = 25;
    public const int PAGINA_PADRAO = 1;

    protected MainController(IMediator mediator, NotificationContext notificacaoContext)
    {
        _mediator = mediator;
        _notificacaoContext = notificacaoContext;
    }

    protected async Task<CreatedAtActionResult> ResponseCreatedAsync(string actionName, object value = null)
        => await Task.FromResult(CreatedAtAction(actionName, value));

    protected async Task<CreatedAtActionResult> ResponseCreatedAsync(string actionName, string controllerName,
        object routeValues, object value = null)
        => await Task.FromResult(CreatedAtAction(actionName, controllerName, routeValues, value));

    protected async Task<IActionResult> ResponseOkAsync<T>(T resultado)
        => await Task.FromResult(Ok(resultado));

    protected async Task<ActionResult<T>> ResponseOk<T>(T resultado)
        => await Task.FromResult(Ok(resultado));


    protected async Task<IActionResult> ResponseNotFoundAsync(string codigo = null, string mensagem = null)
    {
        if (!string.IsNullOrEmpty(mensagem))
        {
            _notificacaoContext.NotFound(codigo, mensagem);
        }

        return await Task.FromResult(NotFound());
    }

    protected async Task<ActionResult> ResponseNotFound(string codigo = null, string mensagem = null)
    {
        if (!string.IsNullOrEmpty(mensagem))
        {
            _notificacaoContext.NotFound(codigo, mensagem);
        }

        return await Task.FromResult(NotFound());
    }

    protected async Task<IActionResult> ResponseBadRequestAsync(string message = null)
    {
        if (message != null)
        {
            _notificacaoContext.BadRequest(string.Empty, message);
        }

        return await Task.FromResult(BadRequest());
    }

    protected async Task<ActionResult> ResponseBadRequest(string message = null)
    {
        if (message != null)
        {
            _notificacaoContext.BadRequest(string.Empty, message);
        }

        return await Task.FromResult(BadRequest());
    }

    protected async Task<IActionResult> ResponseNoContentAsync()
        => await Task.FromResult(NoContent());

    protected async Task<ActionResult> ResponseNotifications()
        => await Task.FromResult(BadRequest());

    protected async Task<IActionResult> ResultadoPaginadoAsync<T>(ResultadoPaginadoViewModel<T> resultadoPaginado)
        => await Task.FromResult(Ok(resultadoPaginado));

    protected async Task<ActionResult<ResultadoPaginadoViewModel<T>>> ResultadoPaginado<T>(
        ResultadoPaginadoViewModel<T> resultadoPaginado)
        => await Task.FromResult(Ok(resultadoPaginado));
    protected bool OperacaoValida()
        => !_notificacaoContext.PossuiNotificacoes && (_notificacaoContext.Notificacoes.Any());

    protected async Task<ActionResult<ResultadoPaginadoViewModel<TViewModelResult>>> ExecutarQueryPaginada<
        TViewModelResult, TQuery>(
        [FromQuery(Name = "frase")] string frase = null,
        [FromQuery(Name = "pagina")] int pagina = PAGINA_PADRAO,
        [FromQuery(Name = "itensPorPagina")] int itensPorPagina = ITENS_POR_PAGINA_PADRAO)
        where TQuery : QueryPaginada<ResultadoPaginadoViewModel<TViewModelResult>>
    {
        var query = (TQuery) Activator.CreateInstance(typeof(TQuery), frase, pagina, itensPorPagina);
        return await ResultadoPaginado(await _mediator.Send(query));
    }

    protected async Task<ActionResult<ResultadoPaginadoViewModel<TViewModelResult>>> ExecutarQueryPaginada<
        TViewModelResult>(
        QueryPaginada<ResultadoPaginadoViewModel<TViewModelResult>> query
    )
    {
        return await ResultadoPaginado(await _mediator.Send(query));
    }

    protected async Task<ActionResult> ExecutarCommandCadastro(Command command, string actionName)
    {
        if (command == null)
            return new BadRequestObjectResult(Mensagens.ObjetoEntradaInvalido);

        var isSuccess = await _mediator.Send(command);

        if (!isSuccess)
            return await ResponseNotifications();

        return await ResponseCreatedAsync(actionName ?? nameof(ExecutarCommandCadastro));
    }

    protected async Task<ActionResult<TViewModel>> ExecutarCommandCadastro<TViewModel>(Command<TViewModel> command,
        string actionName)
        where TViewModel : BaseViewModel
    {
        if (command == null)
            return new BadRequestObjectResult(Mensagens.ObjetoEntradaInvalido);

        var (isSuccess, result) = await _mediator.Send(command);

        if (!isSuccess)
            return await ResponseNotifications();

        return await ResponseCreatedAsync(
            actionName ?? nameof(ExecutarCommandCadastro), result);
    }

    protected async Task<ActionResult<TViewModel>> ExecutarCommandCadastro<TViewModel>(
        Command command,
        Query<TViewModel> query,
        string actionName,
        object routeValues = null
    )
    {
        if (command == null)
            return new BadRequestObjectResult(Mensagens.ObjetoEntradaInvalido);

        if (!await _mediator.Send(command))
            return await ResponseNotifications();

        var queryResult = await _mediator.Send(query);

        return await ResponseCreatedAsync(
            actionName ?? nameof(ExecutarCommandCadastro), queryResult);
    }

    protected async Task<ActionResult<TViewModel>> ExecutarCommandAtualizacao<TViewModel>(
        Command<TViewModel> command
    )
        where TViewModel : BaseViewModel
    {
        if (command == null)
            return new BadRequestObjectResult(Mensagens.ObjetoEntradaInvalido);

        var (isSuccess, result) = await _mediator.Send(command);

        if (!isSuccess)
            return await ResponseNotifications();

        return await ResponseOk(result);
    }

    protected async Task<ActionResult<TViewModel>> ExecutarCommandAtualizacao<TViewModel>(
        Command command,
        IQuery<TViewModel> query
    )
    {
        if (command == null)
            return new BadRequestObjectResult(Mensagens.ObjetoEntradaInvalido);

        if (!await _mediator.Send(command))
            return await ResponseNotifications();

        var queryResult = await _mediator.Send(query);

        return await ResponseOk(queryResult);
    }

    protected async Task<IActionResult> ExecutarCommandAtualizacao(Command command)
    {
        if (!await _mediator.Send(command))
            return await ResponseNotifications();

        return NoContent();
    }

    protected async Task<ActionResult<TViewModel>> ExecutarQuery<TViewModel, TQuery>(Guid id)
        where TQuery : Query<TViewModel>
    {
        var query = (TQuery) Activator.CreateInstance(typeof(TQuery), id);

        return await ResponseOk(await _mediator.Send(query));
    }

    protected async Task<ActionResult<TViewModel>> ExecutarQuery<TViewModel>(Query<TViewModel> query)
    {
        return await ResponseOk(await _mediator.Send(query));
    }

    protected async Task<IActionResult> ExecutarCommandExclusao<TCommand>(Guid id)
        where TCommand : Command
    {
        var command = (TCommand) Activator.CreateInstance(typeof(TCommand), id);

        if (!await _mediator.Send(command))
            return await ResponseNotifications();

        return NoContent();
    }

    protected async Task<ActionResult> ExecutarCommandExclusao<TCommand>(TCommand command)
        where TCommand : Command
    {
        if (!await _mediator.Send(command))
            return await ResponseNotifications();

        return NoContent();
    }
}
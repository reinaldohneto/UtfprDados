using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Utfpr.Dados.API.Application.Notification;
using Utfpr.Dados.API.Application.ViewModels;

namespace Utfpr.Dados.API.Configurations.Api;

public class AsyncFluentValidationFilter : IAsyncActionFilter
{
    private readonly NotificationContext _notificacaoContext;

    public AsyncFluentValidationFilter(NotificationContext notificacaoContext)
    {
        _notificacaoContext = notificacaoContext;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        CheckErrorMessages(context);

        await next();

        CheckErrorMessages(context);

    }

    private void CheckErrorMessages(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid || _notificacaoContext.PossuiNotificacoes)
        {
            var responseObj = new ResultadoComErroViewModel(_notificacaoContext.ObterNotificacoes());

            if (ExisteNotFound(responseObj))
            {
                context.Result = new NotFoundObjectResult(responseObj)
                {
                    ContentTypes = { "application/problem+json" }
                };
            } 
            else
            {
                context.Result = new BadRequestObjectResult(responseObj)
                {
                    ContentTypes = { "application/problem+json" }
                };
            }
        }
    }

    private bool ExisteNotFound(ResultadoComErroViewModel responseObj)
    {
        return responseObj.Erros.Any(e => e.Status == StatusCodes.Status404NotFound);
    }
}
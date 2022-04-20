using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Utfpr.Dados.API.Application.Notification;
using Utfpr.Dados.API.Application.ViewModels;

namespace Utfpr.Dados.API.Configurations.Api;

public class SyncFluentValidationFilter : IActionFilter
{
    private readonly NotificationContext _context;

    public SyncFluentValidationFilter(NotificationContext context)
    {
        _context = context;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (_context.PossuiNotificacoes)
        {
            var responseObj = new ResultadoComErroViewModel(_context.ObterNotificacoes());
            if (ExisteNotFound(responseObj))
            {
                context.Result = new NotFoundObjectResult(responseObj)
                {
                    ContentTypes = {"application/problem+json"}
                };
            }
            else
            {
                context.Result = new BadRequestObjectResult(responseObj)
                {
                    ContentTypes = {"application/problem+json"}
                };
            }

            return;
        }

        if (!context.ModelState.IsValid)
        {
            context.Result = new BadRequestObjectResult(context.ModelState.Values.SelectMany(v => v.Errors))
            {
                ContentTypes = {"application/problem+json"}
            };
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (_context.PossuiNotificacoes)
        {
            var responseObj = new ResultadoComErroViewModel(_context.ObterNotificacoes());

            if (ExisteNotFound(responseObj))
            {
                context.Result = new NotFoundObjectResult(responseObj)
                {
                    ContentTypes = {"application/problem+json"}
                };
            }
            else
            {
                context.Result = new BadRequestObjectResult(responseObj)
                {
                    ContentTypes = {"application/problem+json"}
                };
            }

            return;
        }

        if (!context.ModelState.IsValid)
        {
            context.Result = new BadRequestObjectResult(context.ModelState.Values.SelectMany(v => v.Errors))
            {
                ContentTypes = {"application/problem+json"}
            };
        }
    }

    private bool ExisteNotFound(ResultadoComErroViewModel responseObj)
    {
        return responseObj.Erros.Any(e => e.Status == StatusCodes.Status404NotFound);
    }
}
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;

namespace Utfpr.Dados.API.Application.Notification;

public class NotificationContext
{
    private readonly List<NotificationMessage> _notificacoes;
    public IReadOnlyCollection<NotificationMessage> Notificacoes => _notificacoes;

    public virtual bool PossuiNotificacoes => _notificacoes.Any();

    public static readonly string[] SIZE_VALIDATORS =
    {
        "MaxLenth", "MinLength", "LessThan", "GreaterThan", "LessThanOrEqualTo", "GreaterThanOrEqualTo"
    };

    public NotificationContext()
    {
        _notificacoes = new List<NotificationMessage>();
    }

    public void Unauthorized(string codigo, string descricao)
    {
        AdicionarNotificationMessage(string.Empty, codigo, string.Empty, descricao, 401);
    }

    public void Forbidden(string codigo, string descricao)
    {
        AdicionarNotificationMessage(string.Empty, codigo, string.Empty, descricao, 403);
    }

    public void UnprocessableEntity(string codigo, string descricao)
    {
        AdicionarNotificationMessage(string.Empty, codigo, string.Empty, descricao, 422);
    }

    public void UnprocessableEntity(ValidationResult validationResult)
    {
        AdicionarNotificacoes(validationResult, 422);
    }

    public void NotFound(string codigo, string descricao)
    {
        AdicionarNotificationMessage(string.Empty, codigo, string.Empty, descricao, 404);
    }

    public void BadRequest(string codigo, string descricao)
    {
        AdicionarNotificationMessage(string.Empty, codigo, string.Empty, descricao, 400);
    }

    public void BadRequest(string campo, string codigo, string descricao)
    {
        AdicionarNotificationMessage(campo, codigo, string.Empty, descricao, 400);
    }

    public void BadRequest(string campo, string codigo, string descricao, string valor)
    {
        AdicionarNotificationMessage(campo, codigo, valor, descricao, 400);
    }

    public void BadRequest(IEnumerable<NotificationMessage> notificacoes)
    {
        AdicionarNotificacoes(notificacoes);
    }
    
    public void BadRequest(IEnumerable<IdentityError> notificacoes)
    {
        AdicionarNotificacoes(notificacoes);
    }

    public void BadRequest(IReadOnlyCollection<NotificationMessage> notificacoes)
    {
        AdicionarNotificacoes(notificacoes);
    }

    public void BadRequest(IList<NotificationMessage> notificacoes)
    {
        AdicionarNotificacoes(notificacoes);
    }

    public void BadRequest(ICollection<NotificationMessage> notificacoes)
    {
        AdicionarNotificacoes(notificacoes);
    }

    public void BadRequest(ValidationResult validationResult)
    {
        AdicionarNotificacoes(validationResult);
    }

    // Private Methods

    private void AdicionarNotificationMessage(string campo, string codigo, string valor, string descricao, int status = 0)
    {
        _notificacoes.Add(new NotificationMessage(campo, codigo, valor, string.Format(descricao, campo, valor), status));
    }

    private void AdicionarNotificacoes(IEnumerable<NotificationMessage> notificacoes)
    {
        _notificacoes.AddRange(notificacoes);
    }
    
    private void AdicionarNotificacoes(IEnumerable<IdentityError> notificacoes)
    {
        ObtemNotificacoesIdentityErrors(notificacoes);
    }

    private void AdicionarNotificacoes(IReadOnlyCollection<NotificationMessage> notificacoes)
    {
        _notificacoes.AddRange(notificacoes);
    }

    private void AdicionarNotificacoes(IList<NotificationMessage> notificacoes)
    {
        _notificacoes.AddRange(notificacoes);
    }

    private void AdicionarNotificacoes(ICollection<NotificationMessage> notificacoes)
    {
        _notificacoes.AddRange(notificacoes);
    }

    private void ObtemNotificacoesIdentityErrors(IEnumerable<IdentityError> errors)
    {
        foreach (var error in errors)
        {
            _notificacoes.Add(
                new NotificationMessage(string.Empty, error.Code, 
                    string.Empty, error.Description, 404));   
        }
    }

    private void AdicionarNotificacoes(ValidationResult validationResult, int status = 400)
    {
        var valor = string.Empty;

        validationResult.Errors.ToList().ForEach(error =>
        {
            var valores = error
                .FormattedMessagePlaceholderValues
                .Where(v => SIZE_VALIDATORS.Contains(v.Key))
                .ToList();

            if (valores.Count > 0)
            {
                valor = valores.Select(v => v.Value.ToString()).First();
            }
            else
            {
                valor = string.Empty;
            }

            AdicionarNotificationMessage(error.PropertyName, error.ErrorCode, valor, error.ErrorMessage, status);
        });
    }

    public virtual IEnumerable<NotificationMessage> ObterNotificacoes()
    {
        return _notificacoes;
    }
}
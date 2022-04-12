using MediatR;

namespace Utfpr.Dados.API.Application;

public interface ICommand : IRequest<bool>
{
}

public interface ICommand<TResponse> : IRequest<CommandResult<TResponse>>
{
}
using MediatR;

namespace Utfpr.Dados.API.Application;


public abstract class Command : ICommand
{
}

public abstract class Command<TResponse> : ICommand<TResponse>
{
}
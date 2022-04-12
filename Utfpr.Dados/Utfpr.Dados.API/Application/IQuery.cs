using MediatR;

namespace Utfpr.Dados.API.Application;

public interface IQuery<TResponse> : IRequest<TResponse>
{
}
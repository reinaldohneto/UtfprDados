using MediatR;

namespace Utfpr.Dados.API.Application;

public abstract class Query<TViewModel> : IRequest<TViewModel>
{
    
}
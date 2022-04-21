using AutoMapper;
using MediatR;
using Utfpr.Dados.API.Application.Notification;
using Utfpr.Dados.API.Application.Organizacao.Queries;
using Utfpr.Dados.API.Application.Organizacao.ViewModels;
using Utfpr.Dados.API.Domain.Organizacoes.Interfaces;

namespace Utfpr.Dados.API.Application.Organizacao.QueryHandlers;

public class ObterOrganizacoesQueryHandler : IRequestHandler<ObterOrganizacoesQuery, ResultadoPaginadoViewModel<OrganizacaoViewModel>>
{
    private readonly NotificationContext _notificationContext;
    private readonly IMapper _mapper;
    private readonly IOrganizacaoRepository _organizacaoRepository;


    public ObterOrganizacoesQueryHandler(NotificationContext notificationContext, 
        IMapper mapper, IOrganizacaoRepository organizacaoRepository)
    {
        _notificationContext = notificationContext;
        _mapper = mapper;
        _organizacaoRepository = organizacaoRepository;
    }

    public async Task<ResultadoPaginadoViewModel<OrganizacaoViewModel>> Handle(ObterOrganizacoesQuery request, CancellationToken cancellationToken)
    {
        var organizacoes = await _organizacaoRepository.ObterTodos();
        
        return new ResultadoPaginadoViewModel<OrganizacaoViewModel>(
            _mapper.Map<ICollection<OrganizacaoViewModel>>(organizacoes), request.Pagina, request.ItensPorPagina, organizacoes.Count);
    }
}
using AutoMapper;
using MediatR;
using Utfpr.Dados.API.Application.Notification;
using Utfpr.Dados.API.Application.Organizacao.Queries;
using Utfpr.Dados.API.Application.Organizacao.ViewModels;
using Utfpr.Dados.API.Domain.Organizacoes.Interfaces;

namespace Utfpr.Dados.API.Application.Organizacao.QueryHandlers;

public class ObterOrganizacaoPorIdQueryHandler : IRequestHandler< ObterOrganizacaoPorIdQuery, OrganizacaoViewModel>
{
    private readonly NotificationContext _notificationContext;
    private readonly IMapper _mapper;
    private readonly IOrganizacaoRepository _organizacaoRepository;

    public ObterOrganizacaoPorIdQueryHandler(IOrganizacaoRepository organizacaoRepository, 
        IMapper mapper, NotificationContext notificationContext)
    {
        _organizacaoRepository = organizacaoRepository;
        _mapper = mapper;
        _notificationContext = notificationContext;
    }

    public async Task<OrganizacaoViewModel> Handle(ObterOrganizacaoPorIdQuery request, CancellationToken cancellationToken)
    {
        var registro = await _organizacaoRepository.ObterPorId(request.Id);

        if (registro != null)
            return _mapper.Map<OrganizacaoViewModel>(registro);
        
        _notificationContext.NotFound(nameof(Mensagens.RegistroNaoEncontrado), 
            string.Format(Mensagens.RegistroNaoEncontrado, "OrganizacaoId"));
        return new OrganizacaoViewModel();
    }
}
using AutoMapper;
using MediatR;
using Utfpr.Dados.API.Application.Notification;
using Utfpr.Dados.API.Application.SolicitacaoProcessamento.Queries;
using Utfpr.Dados.API.Application.SolicitacaoProcessamento.ViewModels;
using Utfpr.Dados.API.Domain.Organizacoes.Interfaces;
using Utfpr.Dados.API.Domain.SolicitacoesProcessamento.Interfaces;

namespace Utfpr.Dados.API.Application.SolicitacaoProcessamento.QueryHandlers;

public class SolicitacaoProcessamentoQueryHandler: IRequestHandler<SolicitacaoProcessamentoQuery, SolicitacaoProcessamentoViewModel>
{
    private readonly NotificationContext _notificationContext;
    private readonly IMapper _mapper;
    private readonly ISolicitacaoProcessamentoRepository _solicitacaoProcessamentoRepository;

    public SolicitacaoProcessamentoQueryHandler(NotificationContext notificationContext, IMapper mapper, ISolicitacaoProcessamentoRepository solicitacaoProcessamentoRepository)
    {
        _notificationContext = notificationContext;
        _mapper = mapper;
        _solicitacaoProcessamentoRepository = solicitacaoProcessamentoRepository;
    }

    public async Task<SolicitacaoProcessamentoViewModel> Handle(SolicitacaoProcessamentoQuery request, CancellationToken cancellationToken)
    {
        var registro = await _solicitacaoProcessamentoRepository.ObterPorId(request.Id);
        
        if (registro != null)
            return _mapper.Map<SolicitacaoProcessamentoViewModel>(registro);
        
        _notificationContext.NotFound(nameof(Mensagens.RegistroNaoEncontrado), 
            string.Format(Mensagens.RegistroNaoEncontrado, "SolicitacaoProcessamentoId"));
        return new SolicitacaoProcessamentoViewModel();
    }
}
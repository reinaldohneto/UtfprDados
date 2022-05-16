using AutoMapper;
using MassTransit;
using MediatR;
using Utfpr.Dados.API.Application.Notification;
using Utfpr.Dados.API.Application.SolicitacaoProcessamento.Commands;
using Utfpr.Dados.API.Application.SolicitacaoProcessamento.Messages;
using Utfpr.Dados.API.Application.SolicitacaoProcessamento.ViewModels;
using Utfpr.Dados.API.Domain.Organizacoes.Interfaces;
using Utfpr.Dados.API.Domain.SolicitacoesProcessamento.Interfaces;

namespace Utfpr.Dados.API.Application.SolicitacaoProcessamento.CommandHandlers;

public class CadastrarSolicitacaoProcessamentoCommandHandler : IRequestHandler<CadastrarSolicitacaoProcessamentoCommand, CommandResult<SolicitacaoProcessamentoViewModel>>
{
    private readonly IOrganizacaoRepository _organizacaoRepository;
    private readonly NotificationContext _notificationContext;
    private readonly ISolicitacaoProcessamentoRepository _processamentoRepository;
    private readonly IMapper _mapper;
    private readonly IPublishEndpoint _publishEndpoint;
    
    public CadastrarSolicitacaoProcessamentoCommandHandler(IOrganizacaoRepository organizacaoRepository, 
        NotificationContext notificationContext, ISolicitacaoProcessamentoRepository processamentoRepository, IMapper mapper, IPublishEndpoint publishEndpoint)
    {
        _organizacaoRepository = organizacaoRepository;
        _notificationContext = notificationContext;
        _processamentoRepository = processamentoRepository;
        _mapper = mapper;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<CommandResult<SolicitacaoProcessamentoViewModel>> Handle(CadastrarSolicitacaoProcessamentoCommand request, CancellationToken cancellationToken)
    {
        if (!await Validacoes(request))
            return new CommandResult<SolicitacaoProcessamentoViewModel>();

        var solicitacao = new Domain.SolicitacoesProcessamento.Entities.SolicitacaoProcessamento();
        solicitacao.OrganizacaoId = request.OrganizacaoId;
        solicitacao.Id = request.Id;
        solicitacao.ConjuntoDadosLink = request.ConjuntoDadosLink;
        solicitacao.ConjuntoDadosNome = request.ConjuntoDadosNome;

        if(!await _processamentoRepository.Adicionar(solicitacao))
            return new CommandResult<SolicitacaoProcessamentoViewModel>();

        await _publishEndpoint.Publish(new IniciarProcessamentoMessage(request.Id, 
            request.ConjuntoDadosLink, request.ConjuntoDadosNome), context => context.SetRoutingKey("solicitacoes-processamento"));
        
        return new CommandResult<SolicitacaoProcessamentoViewModel>(true, 
            _mapper.Map<SolicitacaoProcessamentoViewModel>(solicitacao));
    }

    private async Task<bool> Validacoes(CadastrarSolicitacaoProcessamentoCommand command)
    {
        var teste = await _organizacaoRepository.Existe(command.OrganizacaoId);
        if(!teste)
            _notificationContext.BadRequest(nameof(Mensagens.RegistroNaoEncontrado), 
                string.Format(Mensagens.RegistroNaoEncontrado, "OrganizacaoId"));

        return !_notificationContext.PossuiNotificacoes;
    }
}
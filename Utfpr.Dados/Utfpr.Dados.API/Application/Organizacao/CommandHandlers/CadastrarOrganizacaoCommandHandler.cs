using AutoMapper;
using MediatR;
using Utfpr.Dados.API.Application.Notification;
using Utfpr.Dados.API.Application.Organizacao.Commands;
using Utfpr.Dados.API.Application.Organizacao.ViewModels;
using Utfpr.Dados.API.Domain.Organizacoes.Interfaces;

namespace Utfpr.Dados.API.Application.Organizacao.CommandHandlers;

public class CadastrarOrganizacaoCommandHandler : IRequestHandler<CadastrarOrganizacaoCommand, CommandResult<OrganizacaoViewModel>>
{
    private readonly IOrganizacaoRepository _organizacaoRepository;
    private readonly NotificationContext _notificationContext;
    private readonly IMapper _mapper;
    
    public CadastrarOrganizacaoCommandHandler(IOrganizacaoRepository organizacaoRepository, 
        NotificationContext notificationContext, IMapper mapper)
    {
        _organizacaoRepository = organizacaoRepository;
        _notificationContext = notificationContext;
        _mapper = mapper;
    }

    public async Task<CommandResult<OrganizacaoViewModel>> Handle(CadastrarOrganizacaoCommand command, CancellationToken cancellationToken)
    {
        if (!await Validacoes(command)) return new CommandResult<OrganizacaoViewModel>();

        var registro = new Domain.Organizacoes.Entities.Organizacao(command.Id, command.Nome, command.Descricao);

        var result = await _organizacaoRepository.Adicionar(registro);

        if (result)
            return new CommandResult<OrganizacaoViewModel>(true, 
                _mapper.Map<OrganizacaoViewModel>(registro));

        return new CommandResult<OrganizacaoViewModel>();
    }

    private async Task<bool> Validacoes(CadastrarOrganizacaoCommand command)
    {
        if (await _organizacaoRepository.Existe(f => f.Nome.Equals(command.Nome)))
            _notificationContext.BadRequest(nameof(Mensagens.RegistroComNomeJahExistente), 
                Mensagens.RegistroComNomeJahExistente);

        return !_notificationContext.PossuiNotificacoes;
    }
}
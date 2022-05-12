using AutoMapper;
using MediatR;
using Utfpr.Dados.API.Application.Notification;
using Utfpr.Dados.API.Application.Organizacao.Commands;
using Utfpr.Dados.API.Application.Organizacao.ViewModels;
using Utfpr.Dados.API.Domain.Organizacoes.Interfaces;

namespace Utfpr.Dados.API.Application.Organizacao.CommandHandlers;

public class AtualizarOrganizacaoCommandHandler : IRequestHandler<AtualizarOrganizacaoCommand, CommandResult<OrganizacaoViewModel>>
{
    private readonly IOrganizacaoRepository _organizacaoRepository;
    private readonly NotificationContext _notificationContext;
    private readonly IMapper _mapper;

    public AtualizarOrganizacaoCommandHandler(IOrganizacaoRepository organizacaoRepository, 
        NotificationContext notificationContext, IMapper mapper)
    {
        _organizacaoRepository = organizacaoRepository;
        _notificationContext = notificationContext;
        _mapper = mapper;
    }

    public async Task<CommandResult<OrganizacaoViewModel>> Handle(AtualizarOrganizacaoCommand request, CancellationToken cancellationToken)
    {
        var registro = await _organizacaoRepository.ObterPorId(request.Id);

        if (registro == null)
        {
            _notificationContext.NotFound(nameof(Mensagens.RegistroNaoEncontrado),
                string.Format(Mensagens.RegistroNaoEncontrado, "OrganizacaoId"));
            return new CommandResult<OrganizacaoViewModel>();
        }

        registro.Nome = request.Nome;
        registro.Descricao = request.Descricao;

        await _organizacaoRepository.Atualizar(registro);

        return new CommandResult<OrganizacaoViewModel>(true, 
            _mapper.Map<OrganizacaoViewModel>(registro));
    }
}
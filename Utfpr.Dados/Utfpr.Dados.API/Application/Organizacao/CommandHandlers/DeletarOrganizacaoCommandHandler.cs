using MediatR;
using Utfpr.Dados.API.Application.Notification;
using Utfpr.Dados.API.Application.Organizacao.Commands;
using Utfpr.Dados.API.Domain.Organizacoes.Interfaces;

namespace Utfpr.Dados.API.Application.Organizacao.CommandHandlers;

public class DeletarOrganizacaoCommandHandler : IRequestHandler<DeletarOrganizacaoCommand, bool>
{
    private readonly IOrganizacaoRepository _organizacaoRepository;
    private readonly NotificationContext _notificationContext;

    public DeletarOrganizacaoCommandHandler(IOrganizacaoRepository organizacaoRepository, NotificationContext notificationContext)
    {
        _organizacaoRepository = organizacaoRepository;
        _notificationContext = notificationContext;
    }

    public async Task<bool> Handle(DeletarOrganizacaoCommand request, CancellationToken cancellationToken)
    {
        if (await _organizacaoRepository.ExisteSolicitacaoVinculada(request.Id))
        {
            _notificationContext.BadRequest(nameof(Mensagens.OrganizacaoPossuiVinculoComProcessamentos), 
                Mensagens.OrganizacaoPossuiVinculoComProcessamentos);
            return false;
        }

        await _organizacaoRepository.Deletar(request.Id);
        
        return !_notificationContext.PossuiNotificacoes;
    }
}
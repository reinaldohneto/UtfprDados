using System.Security.Claims;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Utfpr.Dados.API.Application.Notification;
using Utfpr.Dados.API.Application.Usuarios.Commands;
using Utfpr.Dados.API.Application.Usuarios.ViewModels;
using Utfpr.Dados.API.Domain.Usuarios.Entities;

namespace Utfpr.Dados.API.Application.Usuarios.CommandHandlers;

public class CadastrarUsuarioCommandHandler : IRequestHandler<CadastrarUsuarioCommand, CommandResult<UsuarioViewModel>>
{
    private readonly UserManager<Usuario> _userManager;
    private readonly NotificationContext _notificationContext;
    private readonly IMapper _mapper;

    public CadastrarUsuarioCommandHandler(UserManager<Usuario> userManager, 
        NotificationContext notificationContext, IMapper mapper)
    {
        _userManager = userManager;
        _notificationContext = notificationContext;
        _mapper = mapper;
    }

    public async Task<CommandResult<UsuarioViewModel>> Handle(CadastrarUsuarioCommand command, CancellationToken cancellationToken)
    {
        if (!await Validacoes(command))
            return new CommandResult<UsuarioViewModel>();

        var usuario = ObtemUsuario(command);
        
        var resultado = await _userManager.CreateAsync(usuario, command.Password);

        if (resultado.Succeeded)
        {
            Claim claim = new Claim("OrganizacaoId", usuario.OrganizacaoId.ToString());
            resultado = await _userManager.AddClaimAsync(usuario, claim);
            if(resultado.Succeeded)
                return new CommandResult<UsuarioViewModel>(true, _mapper.Map<UsuarioViewModel>(usuario));
        }
        
        _notificationContext.BadRequest(resultado.Errors);
        return new CommandResult<UsuarioViewModel>();
    }

    private async Task<bool> Validacoes(CadastrarUsuarioCommand command)
    {
        if(await _userManager.FindByEmailAsync(command.Email) != null)
            _notificationContext.BadRequest(nameof(Mensagens.EmailJahCadastrado), Mensagens.EmailJahCadastrado);

        return !_notificationContext.PossuiNotificacoes;
    }

    private Usuario ObtemUsuario(CadastrarUsuarioCommand command)
    {
        return new Usuario
        {
            Email = command.Email,
            UserName = command.Email,
            Id = command.Id.ToString(),
            OrganizacaoId = command.OrganizacaoId
        };
    }
}
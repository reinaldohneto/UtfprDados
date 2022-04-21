using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Utfpr.Dados.API.Application.Notification;
using Utfpr.Dados.API.Application.Usuarios.Commands;
using Utfpr.Dados.API.Application.Usuarios.ViewModels;
using Utfpr.Dados.API.Domain.Usuarios.Entities;

namespace Utfpr.Dados.API.Application.Usuarios.CommandHandlers;

public class EfetuarLoginCommandHandler : IRequestHandler<EfetuarLoginCommand, CommandResult<TokenViewModel>>
{
    private readonly SignInManager<Usuario> _signInManager;
    private readonly NotificationContext _notificationContext;
    private readonly UserManager<Usuario> _userManager;
    private readonly IConfiguration _configuration;

    public EfetuarLoginCommandHandler(SignInManager<Usuario> signInManager, 
        NotificationContext notificationContext, UserManager<Usuario> userManager, IConfiguration configuration)
    {
        _signInManager = signInManager;
        _notificationContext = notificationContext;
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task<CommandResult<TokenViewModel>> Handle(EfetuarLoginCommand command, CancellationToken cancellationToken)
    {
        var result = await _signInManager.PasswordSignInAsync(command.Username, command.Password, false, false);

        if (!result.Succeeded)
        {
            _notificationContext.BadRequest(nameof(Mensagens.LoginOuSenhaIncorretos), 
                Mensagens.LoginOuSenhaIncorretos);
            return new CommandResult<TokenViewModel>();
        }

        var usuario = await _userManager.FindByEmailAsync(command.Username);

        if (usuario != null)
            return new CommandResult<TokenViewModel>(true, GeraToken(usuario));

        _notificationContext.BadRequest(nameof(Mensagens.ErroInterno),
            Mensagens.ErroInterno);
        return new CommandResult<TokenViewModel>();
    }

    private TokenViewModel GeraToken(Usuario usuario)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.UniqueName, usuario.UserName),
            new Claim("OrganizacaoId", usuario.OrganizacaoId.ToString())
        };

        var jwtKey = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")!.Equals("Production")
            ? Environment.GetEnvironmentVariable("TOKEN_CONFIGURATION_KEY")
            : _configuration.GetValue<string>("JwtKey");

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey ?? throw new ArgumentNullException("Key")));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

        var expiration = DateTime.UtcNow.AddMinutes(double.Parse(_configuration["TokenConfigurations:Seconds"]));

        JwtSecurityToken token = new JwtSecurityToken(
            issuer: _configuration["TokenConfigurations:Issuer"],
            audience: null,
            claims: claims,
            expires: expiration,
            signingCredentials: creds);

        return new TokenViewModel
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Duracao = expiration
        };
    }
}
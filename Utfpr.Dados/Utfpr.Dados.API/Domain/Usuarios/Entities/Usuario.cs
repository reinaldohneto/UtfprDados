using Microsoft.AspNetCore.Identity;
using Utfpr.Dados.API.Domain.Organizacoes.Entities;

namespace Utfpr.Dados.API.Domain.Usuarios.Entities;

public class Usuario : IdentityUser
{
    public Guid OrganizacaoId { get; set; }
    public virtual Organizacao Organizacao { get; set; }
}
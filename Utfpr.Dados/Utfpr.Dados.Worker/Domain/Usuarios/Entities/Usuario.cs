using Microsoft.AspNetCore.Identity;
using Utfpr.Dados.Worker.Domain.Organizacoes.Entities;

namespace Utfpr.Dados.Worker.Domain.Usuarios.Entities;

public class Usuario : IdentityUser
{
    public Guid OrganizacaoId { get; set; }
    public virtual Organizacao Organizacao { get; set; }
}
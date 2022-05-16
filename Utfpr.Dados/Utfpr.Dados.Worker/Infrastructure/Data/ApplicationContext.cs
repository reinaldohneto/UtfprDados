using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Utfpr.Dados.Worker.Domain.Organizacoes.Entities;
using Utfpr.Dados.Worker.Domain.SolicitacoesProcessamento.Entities;
using Utfpr.Dados.Worker.Domain.Usuarios.Entities;
using Utfpr.Dados.Worker.Infrastructure.Data.Maps;

namespace Utfpr.Dados.Worker.Infrastructure.Data;

public class ApplicationContext : IdentityDbContext<Usuario>
{
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Organizacao> Organizacoes { get; set; }
    
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
    }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new OrganizacaoMap());
        builder.ApplyConfiguration(new SolicitacaoProcessamentoMap());
        builder.ApplyConfiguration(new UsuarioMap());

        base.OnModelCreating(builder);
    }
}
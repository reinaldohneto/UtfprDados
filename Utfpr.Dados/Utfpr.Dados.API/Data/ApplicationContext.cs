using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Utfpr.Dados.API.Domain.Organizacoes.Entities;
using Utfpr.Dados.API.Domain.Usuarios.Entities;

namespace Utfpr.Dados.API.Data;

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
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
}
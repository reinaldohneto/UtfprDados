using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Utfpr.Dados.API.Domain.Usuarios.Entities;

namespace Utfpr.Dados.API.Data.Maps;

public class UsuarioMap : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder
            .HasOne(u => u.Organizacao)
            .WithMany(o => o.Usuarios)
            .HasForeignKey(u => u.OrganizacaoId);

        builder.ToTable(nameof(Usuario));
    }
}
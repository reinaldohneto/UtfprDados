using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Utfpr.Dados.API.Domain.Organizacoes.Entities;

namespace Utfpr.Dados.API.Data.Maps;

public class OrganizacaoMap : BaseEntityConfiguration<Organizacao>
{
    public override void Configure(EntityTypeBuilder<Organizacao> builder)
    {
        base.Configure(builder);

        builder
            .Property(o => o.Nome)
            .IsRequired();

        builder
            .Property(o => o.Descricao);

        builder
            .HasMany(o => o.Usuarios)
            .WithOne(u => u.Organizacao);

        builder.ToTable(nameof(Organizacao));
    }
}
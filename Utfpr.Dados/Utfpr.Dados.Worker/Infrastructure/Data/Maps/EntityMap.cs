using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Utfpr.Dados.Worker.Domain;

namespace Utfpr.Dados.Worker.Infrastructure.Data.Maps;

public class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : Entity
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.CadastradoEm)
            .HasDefaultValueSql("NOW()");
    }
}
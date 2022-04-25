using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Utfpr.Dados.API.Domain.SolicitacoesProcessamento.Entities;

namespace Utfpr.Dados.API.Data.Maps;

public class SolicitacaoProcessamentoMap : BaseEntityConfiguration<SolicitacaoProcessamento> 
{
    public override void Configure(EntityTypeBuilder<SolicitacaoProcessamento> builder)
    {
        base.Configure(builder);

        builder
            .Property(p => p.ConjuntoDadosNome)
            .IsRequired();

        builder
            .Property(p => p.ConjuntoDadosLink)
            .IsRequired();
        
        builder
            .Property(p => p.LinkPrivadoBucket);
        
        builder
            .Property(p => p.ProcessamentoStatus)
            .IsRequired();

        builder
            .HasOne(p => p.Organizacao)
            .WithMany(p => p.SolicitacoesProcessamento)
            .HasForeignKey(p => p.OrganizacaoId);
    }
}
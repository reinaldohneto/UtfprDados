using Utfpr.Dados.Worker.Application.Notification;
using Utfpr.Dados.Worker.Application.Services.CompressionService;
using Utfpr.Dados.Worker.Application.Services.DownloadFileService;
using Utfpr.Dados.Worker.Domain.Organizacoes.Interfaces;
using Utfpr.Dados.Worker.Domain.SolicitacoesProcessamento.Interfaces;
using Utfpr.Dados.Worker.Infrastructure.Data.Repositories;

namespace Utfpr.Dados.Worker.Configuration;

public static class DependencyInjectionConfiguration
{
    public static void ConfigureDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IDownloadFileService, DownloadFileService>();
        services.AddScoped<ICompressionService, CompressionService>();

        services.AddScoped<NotificationContext>();
        services.AddScoped<IOrganizacaoRepository, OrganizacaoRepository>();
        services.AddScoped<ISolicitacaoProcessamentoRepository, SolicitacaoProcessamentoRepository>();
    }
}
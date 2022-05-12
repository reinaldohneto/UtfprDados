using System.Reflection;
using FluentValidation.AspNetCore;
using MediatR;
using Utfpr.Dados.API.Application.Notification;
using Utfpr.Dados.API.Data.Repositories;
using Utfpr.Dados.API.Domain;
using Utfpr.Dados.API.Domain.Organizacoes.Interfaces;
using Utfpr.Dados.API.Domain.SolicitacoesProcessamento.Interfaces;

namespace Utfpr.Dados.API.Configurations;

public static class InjectorsConfig
{
    public static void DependencyInjectionConfiguration(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddFluentValidation();

        services.AddScoped<NotificationContext>();
        services.AddScoped<IOrganizacaoRepository, OrganizacaoRepository>();
        services.AddScoped<ISolicitacaoProcessamentoRepository, SolicitacaoProcessamentoRepository>();
    }
}
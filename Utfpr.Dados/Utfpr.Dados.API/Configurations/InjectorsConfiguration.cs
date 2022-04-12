using System.Reflection;
using FluentValidation.AspNetCore;
using MediatR;
using Utfpr.Dados.API.Application.Notification;

namespace Utfpr.Dados.API.Configurations;

public static class InjectorsConfig
{
    public static void DependencyInjectionConfiguration(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddFluentValidation();

        services.AddScoped<NotificationContext>();
    }
}
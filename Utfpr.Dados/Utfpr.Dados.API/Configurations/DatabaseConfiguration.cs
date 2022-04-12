using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Utfpr.Dados.API.Data;
using Utfpr.Dados.API.Domain.Usuarios.Entities;

namespace Utfpr.Dados.API.Configurations;

public static class DatabaseConfiguration
{
    public static void ConfigureDatabase(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<ApplicationContext>(
            options => options.UseNpgsql(config.GetConnectionString("DefaultConnection")));

        services.AddIdentity<Usuario, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationContext>()
            .AddDefaultTokenProviders();
    }
}
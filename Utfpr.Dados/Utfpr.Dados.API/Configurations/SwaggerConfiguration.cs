using Microsoft.OpenApi.Models;

namespace Utfpr.Dados.API.Configurations;

public static class SwaggerConfiguration
{
    public static void AdicionarAutenticacaoSwagger(this IServiceCollection services)
    { 
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "UTFPR DADOS",
                Version = "v1"
            });
            
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme. " +
                              "\r\n\r\nEnter 'Bearer' [space] and then your token in the text input below." +
                              "\r\n\r\nExample: Bearer 123token"
            });
            
            c.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            });
        });
    }
}
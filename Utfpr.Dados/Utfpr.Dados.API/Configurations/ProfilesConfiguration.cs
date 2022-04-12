using AutoMapper;
using Utfpr.Dados.API.Application.Usuarios;

namespace Utfpr.Dados.API.Configurations;

public static class ProfilesConfiguration
{
    public static void AddProfilesConfiguration(this IServiceCollection services)
    {
        var mapperConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new UsuarioAutoMapper());
        });

        IMapper mapper = mapperConfig.CreateMapper();
        services.AddSingleton(mapper);
    }
}
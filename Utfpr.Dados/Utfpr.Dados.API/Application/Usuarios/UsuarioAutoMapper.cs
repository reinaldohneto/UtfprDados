using AutoMapper;
using Utfpr.Dados.API.Application.Usuarios.ViewModels;
using Utfpr.Dados.API.Domain.Usuarios.Entities;

namespace Utfpr.Dados.API.Application.Usuarios;

public class UsuarioAutoMapper : Profile
{
    public UsuarioAutoMapper()
    {
        CreateMap<Usuario, UsuarioViewModel>();
    }
}
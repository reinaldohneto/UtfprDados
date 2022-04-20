using AutoMapper;
using Utfpr.Dados.API.Application.Organizacao.ViewModels;

namespace Utfpr.Dados.API.Application.Organizacao;

public class OrganizacaoAutoMapper : Profile
{
    public OrganizacaoAutoMapper()
    {
        CreateMap<Domain.Organizacoes.Entities.Organizacao, OrganizacaoViewModel>();
    }
}
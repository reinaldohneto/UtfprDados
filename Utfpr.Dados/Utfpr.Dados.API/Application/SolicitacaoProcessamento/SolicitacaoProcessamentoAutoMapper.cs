using AutoMapper;
using Utfpr.Dados.API.Application.SolicitacaoProcessamento.ViewModels;

namespace Utfpr.Dados.API.Application.SolicitacaoProcessamento;

public class SolicitacaoProcessamentoAutoMapper : Profile
{
    public SolicitacaoProcessamentoAutoMapper()
    {
        CreateMap<Domain.SolicitacoesProcessamento.Entities.SolicitacaoProcessamento,
            SolicitacaoProcessamentoViewModel>();
    }
}
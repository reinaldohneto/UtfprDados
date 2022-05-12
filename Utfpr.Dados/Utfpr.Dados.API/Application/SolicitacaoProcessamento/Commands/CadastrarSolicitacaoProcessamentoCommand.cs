using System.ComponentModel.DataAnnotations.Schema;
using AutoMapper;
using Utfpr.Dados.API.Application.SolicitacaoProcessamento.ViewModels;

namespace Utfpr.Dados.API.Application.SolicitacaoProcessamento.Commands;

public class CadastrarSolicitacaoProcessamentoCommand : Command<SolicitacaoProcessamentoViewModel>
{
    [NotMapped] 
    public Guid Id { get; private set; }
    public string ConjuntoDadosNome { get; set; }
    public Uri ConjuntoDadosLink { get; set; }
    public Guid OrganizacaoId { get; set; }

    public CadastrarSolicitacaoProcessamentoCommand()
    {
        Id = Guid.NewGuid();
    }
}
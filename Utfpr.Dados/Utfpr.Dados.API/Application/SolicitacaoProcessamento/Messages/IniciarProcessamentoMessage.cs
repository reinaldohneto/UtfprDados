namespace Utfpr.Dados.API.Application.SolicitacaoProcessamento.Messages;

public class IniciarProcessamentoMessage
{
    public Guid ProcessamentoId { get; init; }
    public Uri ConjuntoDadosLink { get; init; }
    public string ConjuntoDadosNome { get; init; }

    public IniciarProcessamentoMessage(Guid processamentoId, Uri conjuntoDadosLink, string conjuntoDadosNome)
    {
        ProcessamentoId = processamentoId;
        ConjuntoDadosLink = conjuntoDadosLink;
        ConjuntoDadosNome = conjuntoDadosNome;
    }

    public IniciarProcessamentoMessage()
    {
    }
}
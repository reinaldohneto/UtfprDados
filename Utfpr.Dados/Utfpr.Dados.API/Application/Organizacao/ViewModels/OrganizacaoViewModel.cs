namespace Utfpr.Dados.API.Application.Organizacao.ViewModels;

public class OrganizacaoViewModel : BaseViewModel
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string? Descricao { get; set; }
}
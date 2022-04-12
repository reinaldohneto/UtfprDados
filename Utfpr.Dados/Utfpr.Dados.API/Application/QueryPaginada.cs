namespace Utfpr.Dados.API.Application;

public class QueryPaginada<T> : IQuery<T>
{
    public int Pagina { get; set; }
    public int ItensPorPagina { get; set; }

    public QueryPaginada(int pagina, int itensPorPagina)
    {
        Pagina = pagina;
        ItensPorPagina = itensPorPagina;
    }
}
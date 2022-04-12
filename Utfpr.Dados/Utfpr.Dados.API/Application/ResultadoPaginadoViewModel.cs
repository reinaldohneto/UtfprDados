using System.Collections.Generic;

namespace Utfpr.Dados.API.Application;

public class ResultadoPaginadoViewModel<T>
{
    public static ResultadoPaginadoViewModel<T> Vazio(int pagina, int itensPorPagina)
        => new ResultadoPaginadoViewModel<T>(
            new List<T>(),
            pagina,
            itensPorPagina,
            0
        );

    public ICollection<T> Itens { get; set; }
    public int Pagina { get; }
    public int ItensPorPagina { get; }
    public int Total { get; set; }

    public ResultadoPaginadoViewModel(ICollection<T> itens, int pagina, int itensPorPagina, int total)
    {
        Itens = itens ?? new List<T>();
        Pagina = pagina;
        ItensPorPagina = itensPorPagina;
        Total = total;
    }
}
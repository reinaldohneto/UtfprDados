namespace Utfpr.Dados.Worker.Application.Services.CompressionService;

public interface ICompressionService
{
    Task<bool> CompressTextFile(string fileName, Guid processamentoId);
}
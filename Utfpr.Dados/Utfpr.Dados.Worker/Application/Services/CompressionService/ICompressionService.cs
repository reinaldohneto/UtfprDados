namespace Utfpr.Dados.Worker.Application.Services.CompressionService;

public interface ICompressionService
{
    Task<bool> CompressTextFile(string fileName, string folder, Guid processamentoId);
    Task<bool> DecompressTextFile(string fileName, string folder, Guid processamentoId);
}
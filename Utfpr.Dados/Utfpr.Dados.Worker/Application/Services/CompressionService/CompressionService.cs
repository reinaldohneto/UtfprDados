using System.IO.Compression;

namespace Utfpr.Dados.Worker.Application.Services.CompressionService;

public class CompressionService : ICompressionService
{
    private readonly ILogger<CompressionService> _logger;
    
    public CompressionService(ILogger<CompressionService> logger)
    {
        _logger = logger;
    }

    public async Task<bool> CompressTextFile(string fileName, string folder, Guid processamentoId)
    {
        try
        {
            await using var stream = File.OpenRead(folder + fileName);
            await using var fileStream = File.Create(folder + processamentoId + fileName);
            await using var brotliStream = new BrotliStream(fileStream, CompressionLevel.SmallestSize);
            await stream.CopyToAsync(brotliStream);

            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return false;
        }
    }

    public async Task<bool> DecompressTextFile(string fileName, string folder, Guid processamentoId)
    {
        try
        {
            await using FileStream stream = File.OpenRead(folder + processamentoId + fileName);
            byte[] buffer = new byte[4096];
            int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
            await using FileStream fileStream = File.Create(folder + "descomprimido_" + processamentoId + fileName);
            while (bytesRead > 0)
            {
                await using var input = new MemoryStream(buffer);
                await using var brotliStream = new BrotliStream(input, CompressionMode.Decompress);

                await brotliStream.ReadAsync(buffer, 0, buffer.Length);

                await fileStream.WriteAsync(buffer);
                bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
            }

            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return false;
        }   
    }
}
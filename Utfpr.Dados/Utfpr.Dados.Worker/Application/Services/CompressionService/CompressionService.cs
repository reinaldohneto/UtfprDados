using System.IO.Compression;
using System.Text;
using Microsoft.AspNetCore.WebUtilities;

namespace Utfpr.Dados.Worker.Application.Services.CompressionService;

public class CompressionService : ICompressionService
{
    private readonly ILogger<CompressionService> _logger;
    
    public CompressionService(ILogger<CompressionService> logger)
    {
        _logger = logger;
    }

    public async Task<bool> CompressTextFile(string fileName, Guid processamentoId)
    {
        var folder = Environment.GetEnvironmentVariable("DOWNLOAD_FOLDER");
        try
        {
            using (FileStream stream = File.OpenRead(folder + fileName))
            {
                byte[] buffer = new byte[4096];
                int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                using (FileStream fileStream = File.Create(folder + processamentoId + fileName))
                {
                    while (bytesRead > 0)
                    {
                        await using var input = new MemoryStream(buffer);
                        await using var brotliStream = new BrotliStream(fileStream, CompressionLevel.SmallestSize);

                        await input.CopyToAsync(brotliStream);
                        await brotliStream.FlushAsync();

                        await fileStream.WriteAsync(buffer);
                        bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                    }
                }
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
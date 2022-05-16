using System.Net;
using Utfpr.Dados.Worker.Application.Services.CompressionService;

namespace Utfpr.Dados.Worker.Application.Services.DownloadFileService;

public class DownloadFileService : IDownloadFileService
{

    public async Task<bool> DownloadFile(Uri fileUrl, string conjuntoDadosNome)
    {
        if (!Validacoes(fileUrl))
            return false;

        DateTime startTime = DateTime.UtcNow;
        HttpClient request = new HttpClient();
        using(Stream responseStream = await request.GetStreamAsync(fileUrl))
        using (Stream fileStream = File.OpenWrite( Environment.GetEnvironmentVariable("DOWNLOAD_FOLDER") + conjuntoDadosNome))
        {
            byte[] buffer = new byte[4096];
            int bytesRead = await responseStream.ReadAsync(buffer, 0, 4096);
            while (bytesRead > 0)
            {
                fileStream.Write(buffer, 0, bytesRead);
                DateTime nowTime = DateTime.UtcNow;
                if ((nowTime - startTime).TotalMinutes > 5)
                {
                    throw new ApplicationException("Download timed out");
                }

                bytesRead = await responseStream.ReadAsync(buffer, 0, 4096);
            }
        }
        return true;
    }

    private bool Validacoes(Uri fileUrl)
    {
        return (!fileUrl.OriginalString.Contains("json") || !fileUrl.OriginalString.Contains("csv"));
    }
}
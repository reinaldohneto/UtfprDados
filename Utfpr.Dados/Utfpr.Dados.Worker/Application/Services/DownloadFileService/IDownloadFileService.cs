namespace Utfpr.Dados.Worker.Application.Services.DownloadFileService;

public interface IDownloadFileService
{
    Task<bool> DownloadFile(Uri fileUrl, string conjuntoDadosNome);
}
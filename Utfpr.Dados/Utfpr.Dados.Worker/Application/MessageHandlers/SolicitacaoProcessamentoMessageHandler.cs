using Amazon.S3;
using Amazon.S3.Model;
using MassTransit;
using Utfpr.Dados.Messages.Messages;
using Utfpr.Dados.Worker.Application.Services.CompressionService;
using Utfpr.Dados.Worker.Application.Services.DownloadFileService;
using Utfpr.Dados.Worker.Domain.SolicitacoesProcessamento.Enums;
using Utfpr.Dados.Worker.Domain.SolicitacoesProcessamento.Interfaces;

namespace Utfpr.Dados.Worker.Application.MessageHandlers;

public class SolicitacaoProcessamentoMessageHandler : IConsumer<IniciarProcessamentoMessage>
{
    private readonly IDownloadFileService _downloadFileService;
    private readonly ICompressionService _compressionService;
    private readonly ISolicitacaoProcessamentoRepository _processamentoRepository;
    private readonly ILogger<SolicitacaoProcessamentoMessageHandler> _logger;
//    private readonly IAmazonS3 _s3Client;

    public SolicitacaoProcessamentoMessageHandler(IDownloadFileService downloadFileService, 
        ICompressionService compressionService, ISolicitacaoProcessamentoRepository processamentoRepository, 
        ILogger<SolicitacaoProcessamentoMessageHandler> logger/*, IAmazonS3 s3Client*/)
    {
        _downloadFileService = downloadFileService;
        _compressionService = compressionService;
        _processamentoRepository = processamentoRepository;
        _logger = logger;
        /*_s3Client = s3Client;*/
    }

    public async Task Consume(ConsumeContext<IniciarProcessamentoMessage> context)
    {
        var processamento = await _processamentoRepository.ObterPorId(context.Message.ProcessamentoId);

        if (processamento == null)
            return;

        processamento.ProcessamentoStatus = SolicitacaoProcessamentoStatus.EM_ANDAMENTO;
        var folder = Environment.GetEnvironmentVariable("DOWNLOAD_FOLDER") ?? throw new ApplicationException("DOWNLOAD_FOLDER cannot be null");

        if(!await _processamentoRepository.Atualizar(processamento))
            _logger.LogError(new ApplicationException(Mensagens.ErroInterno), Mensagens.ErroInterno);

        if (!await _downloadFileService.DownloadFile(context.Message.ConjuntoDadosLink,
                context.Message.ConjuntoDadosNome, folder))
        {
            _logger.LogInformation("Dados de entrada da mensagem inválido" + 
                                   context.MessageId + "\n \n" + context.Message.ProcessamentoId);
            processamento.ProcessamentoStatus = SolicitacaoProcessamentoStatus.FALHOU;
            await _processamentoRepository.Atualizar(processamento);
        }

        await _compressionService.CompressTextFile(context.Message.ConjuntoDadosNome, folder, context.Message.ProcessamentoId);

        await _compressionService.DecompressTextFile(context.Message.ConjuntoDadosNome, folder,
            context.Message.ProcessamentoId);
        /*
        using (var stream =
               File.OpenRead(folder + context.Message.ProcessamentoId + context.Message.ConjuntoDadosNome))
        {
            var putRequest = new PutObjectRequest
            {
                Key = context.Message.ProcessamentoId + context.Message.ConjuntoDadosNome,
                BucketName = "reinaldo-utfpr-tcc",
                InputStream = stream,
                AutoCloseStream = true
            };
            var respose = await _s3Client.PutObjectAsync(putRequest);
        }
    */
    }
}
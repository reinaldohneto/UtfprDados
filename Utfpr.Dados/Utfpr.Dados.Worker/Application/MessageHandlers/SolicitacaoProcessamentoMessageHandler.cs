using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
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
    private readonly IAmazonS3 _amazonS3;
    private readonly TransferUtility _transferUtility;
    
    public SolicitacaoProcessamentoMessageHandler(IDownloadFileService downloadFileService, 
        ICompressionService compressionService, ISolicitacaoProcessamentoRepository processamentoRepository, 
        ILogger<SolicitacaoProcessamentoMessageHandler> logger)
    {
        _downloadFileService = downloadFileService;
        _compressionService = compressionService;
        _processamentoRepository = processamentoRepository;
        _logger = logger;
        _amazonS3 = new AmazonS3Client(RegionEndpoint.USEast1);
        _transferUtility = new TransferUtility(_amazonS3);
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

        TransferUtilityUploadRequest request = new TransferUtilityUploadRequest();

        request.BucketName = string.Format("{0}{1}", Environment.GetEnvironmentVariable("BUCKET_NAME"), 
            context.Message.ConjuntoDadosNome);
        request.Key = context.Message.ConjuntoDadosNome;
        request.InputStream = File.OpenRead(folder + context.Message.ConjuntoDadosNome);
        await _transferUtility.UploadAsync(request);
    }
}
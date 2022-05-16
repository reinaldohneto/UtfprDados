using MassTransit;
using Utfpr.Dados.Worker.Application.Services.CompressionService;
using Utfpr.Dados.Worker.Application.Services.DownloadFileService;
using Utfpr.Dados.Worker.Domain.SolicitacoesProcessamento.Enums;
using Utfpr.Dados.Worker.Domain.SolicitacoesProcessamento.Interfaces;

namespace Utfpr.Dados.Worker.Application.MessageHandlers;

public class SolicitacaoProcessamentoMessageHandler : IConsumer<API.Application.SolicitacaoProcessamento.Messages.IniciarProcessamentoMessage>
{
    private readonly IDownloadFileService _downloadFileService;
    private readonly ICompressionService _compressionService;
    private readonly ISolicitacaoProcessamentoRepository _processamentoRepository;
    private readonly ILogger<SolicitacaoProcessamentoMessageHandler> _logger;

    public SolicitacaoProcessamentoMessageHandler(IDownloadFileService downloadFileService, 
        ICompressionService compressionService, ISolicitacaoProcessamentoRepository processamentoRepository, 
        ILogger<SolicitacaoProcessamentoMessageHandler> logger)
    {
        _downloadFileService = downloadFileService;
        _compressionService = compressionService;
        _processamentoRepository = processamentoRepository;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<API.Application.SolicitacaoProcessamento.Messages.IniciarProcessamentoMessage> context)
    {
        var processamento = await _processamentoRepository.ObterPorId(context.Message.ProcessamentoId);

        if (processamento == null)
            return;

        processamento.ProcessamentoStatus = SolicitacaoProcessamentoStatus.EM_ANDAMENTO;

        if(!await _processamentoRepository.Atualizar(processamento))
            _logger.LogError(new ApplicationException(Mensagens.ErroInterno), Mensagens.ErroInterno);

        if (!await _downloadFileService.DownloadFile(context.Message.ConjuntoDadosLink,
                context.Message.ConjuntoDadosNome))
        {
            _logger.LogInformation("Dados de entrada da mensagem inválido" + 
                                   context.MessageId + "\n \n" + context.Message.ProcessamentoId);
            processamento.ProcessamentoStatus = SolicitacaoProcessamentoStatus.FALHOU;
            await _processamentoRepository.Atualizar(processamento);
        }

        await _compressionService.CompressTextFile(context.Message.ConjuntoDadosNome, context.Message.ProcessamentoId);
    }
}
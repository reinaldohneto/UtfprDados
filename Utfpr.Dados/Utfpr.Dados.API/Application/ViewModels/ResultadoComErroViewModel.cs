using Newtonsoft.Json;
using Utfpr.Dados.API.Application.Notification;

namespace Utfpr.Dados.API.Application.ViewModels;

public class ResultadoComErroViewModel
{
    public NotificationMessage[] Erros { get; set; }
        
    public ResultadoComErroViewModel(IEnumerable<NotificationMessage> erros)
    {
        Erros = erros.ToArray();
    }
        
    public ResultadoComErroViewModel(NotificationMessage erro)
    {
        Erros = new []{ erro };
    }

    public static ResultadoComErroViewModel GetFromJson(string json)
    {
        if (string.IsNullOrEmpty(json))
        {
            return new ResultadoComErroViewModel(new List<NotificationMessage>());
        }
        return JsonConvert.DeserializeObject<ResultadoComErroViewModel>(json);
    }
}
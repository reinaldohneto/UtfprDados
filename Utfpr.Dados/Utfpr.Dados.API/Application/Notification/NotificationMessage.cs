using System.Net;


namespace Utfpr.Dados.API.Application.Notification;
public class NotificationMessage
{
    public NotificationMessage(string campo, string codigo, string valor, string descricao, int status)
    {
        Campo = campo;
        Codigo = codigo;
        Valor = valor;
        Descricao = descricao;
        Status = status;
    }
    
    public string Campo { get; private set; }
    public string Codigo { get; private set; }
    public string Valor { get; private set; }
    public string Descricao { get; private set; }
    public int Status { get; private set; }
}
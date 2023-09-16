namespace Infrastructure.Models;

public class SendMessageModel
{
    public string SenderUsername { get; set; }

    public string ReceiverUsername { get; set; }

    public string Content { get; set; }

    public DateTime DateSent { get; set; }
}
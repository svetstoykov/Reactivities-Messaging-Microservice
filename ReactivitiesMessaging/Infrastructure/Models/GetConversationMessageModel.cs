namespace Infrastructure.Models;

public class GetConversationMessageModel
{
    public string SenderUsername { get; init; }

    public string ReceiverUsername { get; init; }
    
    public int PageIndex { get; init; }
        
    public int PageSize { get; init; }
}
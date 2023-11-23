namespace Infrastructure.Settings;

public class RabbitMqConfiguration
{
    public string SendMessageQueueName { get; set; }
    
    public string MessagingExchangeName { get; set; }
    
    public string GetConversationQueueName { get; set; }
    
    public string GetConversationExchangeName { get; set; }
    
    public string ConnectionString { get; set; }
}
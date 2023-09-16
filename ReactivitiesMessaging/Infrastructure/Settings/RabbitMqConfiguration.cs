namespace Infrastructure.Settings;

public class RabbitMqConfiguration
{
    public string SendMessageQueueName { get; set; }
    
    public string ConnectionString { get; set; }
}
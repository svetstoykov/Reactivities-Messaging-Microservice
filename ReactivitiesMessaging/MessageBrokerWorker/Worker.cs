using System.Text;
using Domain.Models;
using EasyNetQ;

namespace MessageBrokerService;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IBus _rabbitMqBus;

    public Worker(ILogger<Worker> logger, IBus rabbitMqBus)
    {
        _logger = logger;
        _rabbitMqBus = rabbitMqBus;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _rabbitMqBus.Rpc
            .RespondAsync<int, StringBuilder>(_ => 
                new StringBuilder("working?"), cancellationToken: stoppingToken);
    }
}
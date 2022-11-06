using Core.Commands;
using EasyNetQ;
using MediatR;
using Reactivities.Common.Messages.Models.Request;
using Reactivities.Common.Result.Models;

namespace MessageBrokerService;

public class Worker : BackgroundService
{
    private class ErrorMessage
    {
        public const string ForInvalidMediatorScopedService = "Failed to retrieve scoped Mediator service";
    }
    
    private readonly IBus _rabbitMqBus;
    private readonly IServiceProvider _serviceProvider;

    public Worker(IBus rabbitMqBus, IServiceProvider serviceProvider)
    {
        this._rabbitMqBus = rabbitMqBus;
        this._serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await this._rabbitMqBus.Rpc.RespondAsync<SendMessageRequestModel, Result<bool>>(async req =>
            {
                var command = new SendMessage.Command(req.SenderUsername, req.ReceiverUsername, req.Content, req.DateSent);

                return await this.ExecuteScopedMediatorRequestAsync(command);
            }, cancellationToken: stoppingToken);
    }

    private async Task<TResponse> ExecuteScopedMediatorRequestAsync<TResponse>(IRequest<TResponse> mediatorRequest)
    {
        using var scope = this._serviceProvider.CreateScope();
        var mediator = scope.ServiceProvider.GetService<IMediator>();

        if (mediator == null)
        {
            throw new InvalidOperationException(
                ErrorMessage.ForInvalidMediatorScopedService);
        }

        return await mediator.Send(mediatorRequest);
    }
}
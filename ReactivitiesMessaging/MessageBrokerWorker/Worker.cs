using Core.Commands;
using EasyNetQ;
using MediatR;
using Reactivities.Common.Messages.Models.Request;
using Reactivities.Common.Result.Models;

namespace MessageBrokerService;

public class Worker : BackgroundService
{
    private readonly IBus _rabbitMqBus;
    private readonly IMediator _mediator;

    public Worker(IBus rabbitMqBus, IMediator mediator)
    {
        this._rabbitMqBus = rabbitMqBus;
        this._mediator = mediator;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await this._rabbitMqBus.Rpc.RespondAsync<SendMessageRequestModel, Result<bool>>(
            async request => await this._mediator.Send(new SendMessage.Command(
                request.SenderUsername, request.ReceiverUsername, request.Content, request.DateSent), stoppingToken));
    }
}
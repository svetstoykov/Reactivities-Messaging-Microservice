using AutoMapper;
using Core.Commands;
using Core.Queries;
using EasyNetQ;
using MediatR;
using Reactivities.Common.Messages.Models.Request;
using Reactivities.Common.Messages.Models.Response;
using Reactivities.Common.Result.Models;

namespace MessageBrokerService;

public class Worker : BackgroundService
{
    private class ErrorMessage
    {
        public const string ForInvalidMediatorScopedService = "Failed to retrieve scoped Mediator service";
    }

    private readonly IBus _rabbitMqBus;
    private readonly IMapper _mapper;
    private readonly IServiceProvider _serviceProvider;

    public Worker(IBus rabbitMqBus, IMapper mapper, IServiceProvider serviceProvider)
    {
        this._rabbitMqBus = rabbitMqBus;
        this._mapper = mapper;
        this._serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await this._rabbitMqBus.Rpc.RespondAsync<SendMessageRequestModel, Result<bool>>(
            async request => await this.ExecuteScopedMediatorRequestAsync(this._mapper.Map<SendMessage.Command>(request)), 
            cancellationToken: stoppingToken);
        
        await this._rabbitMqBus.Rpc.RespondAsync<GetSenderReceiverConversationRequestModel, Result<SenderReceiverConversationResponseModel>>(
            async request => await this.ExecuteScopedMediatorRequestAsync(this._mapper.Map<GetSenderReceiverConversation.Query>(request)), 
            cancellationToken: stoppingToken);
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
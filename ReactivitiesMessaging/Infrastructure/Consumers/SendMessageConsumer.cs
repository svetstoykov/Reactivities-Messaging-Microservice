using AutoMapper;
using Core.Commands;
using Infrastructure.Models;
using MassTransit;
using MediatR;

namespace Infrastructure.Consumers;

public class SendMessageConsumer : IConsumer<SendMessageModel>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public SendMessageConsumer(IMediator mediator, IMapper mapper)
    {
        this._mediator = mediator;
        this._mapper = mapper;
    }

    public async Task Consume(ConsumeContext<SendMessageModel> context)
    {
        var message = context.Message;
        
        var query = new Core.Commands.SendMessage.Command(
            message.SenderUsername,
            message.ReceiverUsername,
            message.Content,
            message.DateSent);

        await this._mediator.Send(query);
    }
}
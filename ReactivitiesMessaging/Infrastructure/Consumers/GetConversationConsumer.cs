using AutoMapper;
using Core.Queries;
using Infrastructure.Models;
using MassTransit;
using MediatR;

namespace Infrastructure.Consumers;

public class GetConversationConsumer : IConsumer<GetConversationMessageModel>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public GetConversationConsumer(IMediator mediator, IMapper mapper)
    {
        this._mediator = mediator;
        this._mapper = mapper;
    }

    public async Task Consume(ConsumeContext<GetConversationMessageModel> context)
    {
        var query = this._mapper.Map<GetConversation.Query>(context.Message);

        var result = await this._mediator.Send(query);

        await context.RespondAsync<GetConversationMessageModel>(result);
    }
}
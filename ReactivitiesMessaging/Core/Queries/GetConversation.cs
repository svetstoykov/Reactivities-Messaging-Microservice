using Core.Common;
using Core.Interfaces.DataServices;
using Core.Models;
using MediatR;

namespace Core.Queries;

public class GetConversation
{
    public class Query : IRequest<PaginatedResult<MessageOutputModel>>
    {
        public string SenderUsername { get; init; }

        public string ReceiverUsername { get; init; }
    
        public int PageIndex { get; init; }
        
        public int PageSize { get; init; }
    }

    public class Handler : IRequestHandler<Query, PaginatedResult<MessageOutputModel>>
    {
        private readonly IMessagesDataService _messagesDataService;

        public Handler(IMessagesDataService messagesDataService)
        {
            this._messagesDataService = messagesDataService;
        }

        public async Task<PaginatedResult<MessageOutputModel>> Handle(Query request, CancellationToken cancellationToken)
        {
            var paginatedResult = await this._messagesDataService.GetMessagesConversationAsync(
                request.SenderUsername, request.ReceiverUsername, request.PageIndex, request.PageSize);

            return paginatedResult;
        }
    }
}
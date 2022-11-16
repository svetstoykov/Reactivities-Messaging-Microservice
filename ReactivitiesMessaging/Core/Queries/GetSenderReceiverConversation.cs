using MediatR;
using Reactivities.Common.Messages.Models.Response;
using Reactivities.Common.Result.Models;

namespace Core.Queries;

public class GetSenderReceiverConversation
{
    public class Query : IRequest<Result<SenderReceiverConversationResponseModel>>
    {
        private Query()
        { }
        
        public string SenderUsername { get; }
    
        public string ReceiverUsername { get; }
    
        public int InitialMessagesLoadCount { get; }
    
        public DateTime? DateFrom { get; }
    
        public DateTime? DateTo { get; }
    }
    
    public class Handler : IRequestHandler<Query, Result<SenderReceiverConversationResponseModel>>
    {
        public async Task<Result<SenderReceiverConversationResponseModel>> Handle(
            Query request, CancellationToken cancellationToken)
        {
            //TODO Finish implementation
            throw new NotImplementedException();
        }
    }
}
using Core.Common;
using Core.Models;
using Domain.Models;
using Reactivities.Common.DataServices.Abstractions.Interfaces;

namespace Core.Interfaces.DataServices;

public interface IMessagesDataService : IEntityDataService<Message>
{
    Task<PaginatedResult<MessageOutputModel>> GetMessagesConversationAsync(
        string senderUsername, string receiverUsername, int startIndex, int pageSize);
    
    Task SaveMessageAsync(Message message);
}
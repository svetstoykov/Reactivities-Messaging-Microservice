using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Common;
using Core.Interfaces.DataServices;
using Core.Models;
using Domain.Models;
using Infrastructure.Extensions;
using Reactivities.Common.DataServices.Services;

namespace Infrastructure.DataServices;

public class MessagesDataService : EntityDataService<ReactivitiesContext, Message>, IMessagesDataService
{
    private readonly IMapper _mapper;

    public MessagesDataService(ReactivitiesContext dataContext, IMapper mapper)
        : base(dataContext)
    {
        this._mapper = mapper;
    }

    public async Task<PaginatedResult<MessageOutputModel>> GetMessagesConversationAsync(
        string senderUsername,
        string receiverUsername, 
        int startIndex, 
        int pageSize)
    {
        var messagesQuery = this.DataSet
            .Where(m => m.Sender.UserName == senderUsername
                        && m.Receiver.UserName == receiverUsername)
            .OrderByDescending(m => m.DateSent)
            .ProjectTo<MessageOutputModel>(this._mapper.ConfigurationProvider);

        return await messagesQuery.PaginateAsync(pageSize, startIndex);
    }

    public async Task SaveMessageAsync(Message message)
    {
        this.DataSet.Add(message);

        await this.SaveChangesAsync();
    }
}
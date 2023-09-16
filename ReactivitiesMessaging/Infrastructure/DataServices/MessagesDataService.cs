using Core.Interfaces.DataServices;
using Domain.Models;
using Reactivities.Common.DataServices.Services;

namespace Infrastructure.DataServices;

public class MessagesDataService : EntityDataService<ReactivitiesContext, Message>, IMessagesDataService
{
    public MessagesDataService(ReactivitiesContext dataContext)
        : base(dataContext)
    {
    }
    
    public async Task SaveMessageAsync(Message message)
    {
        this.DataSet.Add(message);

        await this.SaveChangesAsync();
    }
}
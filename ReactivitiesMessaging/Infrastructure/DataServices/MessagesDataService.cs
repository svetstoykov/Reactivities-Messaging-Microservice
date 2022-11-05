using Core.Interfaces.DataServices;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Reactivities.Common.DataServices.Services;

namespace Infrastructure.DataServices;

public class MessagesDataService : EntityDataService<ReactivitiesContext, Message>, IMessagesDataService
{
    public MessagesDataService(ReactivitiesContext dataContext) : base(dataContext)
    {
    }
    
    public async Task SaveMessageToDatabaseAsync(Message message)
    {
        this.Create(message);

        await this.SaveChangesAsync();
    }
}
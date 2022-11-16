using Core.Interfaces.DataServices;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Reactivities.Common.DataServices.Services;

namespace Infrastructure.DataServices;

public class MessagesDataService : IMessagesDataService
{
    private readonly ReactivitiesContext _dbContext;

    public MessagesDataService(ReactivitiesContext dbContext)
    {
        this._dbContext = dbContext;
    }

    public async Task SaveMessageToDatabaseAsync(Message message)
    {
        this._dbContext.Messages.Add(message);

        await this._dbContext.SaveChangesAsync();
    }
}
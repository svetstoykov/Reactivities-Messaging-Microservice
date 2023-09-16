using Domain.Models;
using Reactivities.Common.DataServices.Abstractions.Interfaces;

namespace Core.Interfaces.DataServices;

public interface IMessagesDataService : IEntityDataService<Message>
{
    Task SaveMessageAsync(Message message);
}
using Domain.Models;
using Reactivities.Common.DataServices.Abstractions.Interfaces;

namespace Core.Interfaces.DataServices;

public interface IProfilesDataService : IEntityDataService<Profile>
{
    public Task<Profile> GetByUsernameAsync(string username);
}
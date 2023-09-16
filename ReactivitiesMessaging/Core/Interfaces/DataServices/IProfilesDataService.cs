using Domain.Models;

namespace Core.Interfaces.DataServices;

public interface IProfilesDataService
{
    public Task<Profile> GetByUsernameAsync(string username);
}
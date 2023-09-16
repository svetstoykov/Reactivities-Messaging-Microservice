using Core.Interfaces.DataServices;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataServices;

public class ProfilesDataService : IProfilesDataService
{
    private readonly ReactivitiesContext _dbContext;

    public ProfilesDataService(ReactivitiesContext dbContext)
    {
        this._dbContext = dbContext;
    }

    public async Task<Profile> GetByUsernameAsync(string username)
        => await this._dbContext.Profiles.FirstOrDefaultAsync(p => p.UserName.ToLower() == username.ToLower());
}
using Core.Interfaces.DataServices;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Reactivities.Common.DataServices.Services;

namespace Infrastructure.DataServices;

public class ProfilesDataService : EntityDataService<ReactivitiesContext,Profile>,IProfilesDataService
{
    public ProfilesDataService(ReactivitiesContext dataContext) : base(dataContext)
    {
    }
    
    public async Task<Profile> GetByUsernameAsync(string username)
        => await this.DataSet.FirstOrDefaultAsync(p => p.UserName.ToLower() == username.ToLower());
}
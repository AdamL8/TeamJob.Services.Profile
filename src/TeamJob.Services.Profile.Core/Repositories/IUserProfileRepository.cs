using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeamJob.Services.Profile.Core.Entities;

namespace TeamJob.Services.Profile.Core.Repositories
{
    public interface IUserProfileRepository
    {
        Task<UserProfile> GetAsync(Guid id);
        Task DeleteAsync(Guid id);
        Task UpdateAsync(UserProfile userProfile);
        Task AddAsync(UserProfile userProfile);
        Task<List<UserProfile>> GetAllAsync();
    }
}
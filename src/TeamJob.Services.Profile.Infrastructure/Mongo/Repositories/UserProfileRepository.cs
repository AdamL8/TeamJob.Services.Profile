using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Convey.Persistence.MongoDB;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using TeamJob.Services.Profile.Core.Entities;
using TeamJob.Services.Profile.Core.Repositories;
using TeamJob.Services.Profile.Infrastructure.Mongo.Documents;

namespace TeamJob.Services.Profile.Infrastructure.Mongo.Repositories
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly IMongoRepository<UserProfileDocument, Guid> _repository;

        public UserProfileRepository(IMongoRepository<UserProfileDocument, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<UserProfile> GetAsync(Guid id)
        {
            var userProfile = await _repository.GetAsync(id);

            return userProfile?.AsEntity();
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task UpdateAsync(UserProfile userProfile)
        {
            if (userProfile is null)
            { return; }

            await _repository.UpdateAsync(userProfile.AsDocument());
        }

        public async Task AddAsync(UserProfile userProfile)
        {
            if (userProfile is null)
            { return; }

            await _repository.AddAsync(userProfile.AsDocument());
        }

        public async Task<List<UserProfile>> GetAllAsync()
        {
            var profiles = _repository.Collection?.AsQueryable();
            return await profiles.Select(x => x.AsEntity()).ToListAsync();
        }
    }
}

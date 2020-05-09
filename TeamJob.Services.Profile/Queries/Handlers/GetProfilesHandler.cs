using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;
using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Threading.Tasks;
using TeamJob.Services.Profile.Domain;
using TeamJob.Services.Profile.DTO;
using TeamJob.Services.Profile.Exceptions;

namespace TeamJob.Services.Profile.Queries.Handlers
{
    public class GetProfilesHandler : IQueryHandler<GetProfiles, IEnumerable<ProfileDto>>
    {
        private readonly IMongoRepository<UserProfile, Guid> _profileRepository;

        public GetProfilesHandler(IMongoRepository<UserProfile, Guid> InProfileRepository)
        {
            _profileRepository = InProfileRepository;
        }

        public async Task<IEnumerable<ProfileDto>> HandleAsync(GetProfiles InQuery)
        {
            var documents = _profileRepository.Collection.AsQueryable();

            if (string.IsNullOrEmpty(InQuery.Role) == false)
            {
                var profileRole = Enum.Parse<Role>(InQuery.Role);
                documents       = documents.Where(p => p.Role == profileRole);
            }

            var profiles = await documents.ToListAsync();

            return profiles.Select(p => p.AsDto());
        }
    }
}

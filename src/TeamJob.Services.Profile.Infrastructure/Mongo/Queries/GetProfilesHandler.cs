using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using TeamJob.Services.Profile.Application.DTO;
using TeamJob.Services.Profile.Application.Queries;
using TeamJob.Services.Profile.Core.Entities;
using TeamJob.Services.Profile.Core.Repositories;
using TeamJob.Services.Profile.Infrastructure.Mongo.Documents;

namespace TeamJob.Services.Profile.Infrastructure.Mongo.Queries
{
    public class GetProfilesHandler : IQueryHandler<GetProfiles, IEnumerable<UserProfileDetailsDto>>
    {
        private readonly IMongoRepository<UserProfileDocument, Guid> _userProfileRepository;

        public GetProfilesHandler(IMongoRepository<UserProfileDocument, Guid> userProfileRepository)
        {
            _userProfileRepository = userProfileRepository;
        }

        public async Task<IEnumerable<UserProfileDetailsDto>> HandleAsync(GetProfiles query)
        {
            var documents = _userProfileRepository.Collection.AsQueryable();

            if (string.IsNullOrEmpty(query.Role) == false)
            {
                var profileRole = Enum.Parse<Role>(query.Role);
                documents       = Queryable.Where(documents, p => p.Role == profileRole) as IMongoQueryable<UserProfileDocument>;
            }

            var profiles = await documents.ToListAsync();

            return profiles.Select(p => p.AsDetailsDto());
        }
    }
}

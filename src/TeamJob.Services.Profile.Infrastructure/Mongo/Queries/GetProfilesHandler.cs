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
        private readonly IMongoRepository<UserProfileDocument, string> _userProfileRepository;

        public GetProfilesHandler(IMongoRepository<UserProfileDocument, string> userProfileRepository)
        {
            _userProfileRepository = userProfileRepository;
        }

        public async Task<IEnumerable<UserProfileDetailsDto>> HandleAsync(GetProfiles query)
        {
            var documents = string.IsNullOrEmpty(query.Role)
                ? await _userProfileRepository.FindAsync(p => true)
                : await _userProfileRepository.FindAsync(p => p.Role == Enum.Parse<Role>(query.Role));

            return documents.Select(p => p.AsDetailsDto());
        }
    }
}

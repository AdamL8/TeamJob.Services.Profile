using System;
using System.Threading.Tasks;
using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;
using TeamJob.Services.Profile.Application.DTO;
using TeamJob.Services.Profile.Application.Queries;
using TeamJob.Services.Profile.Core.Repositories;
using TeamJob.Services.Profile.Infrastructure.Mongo.Documents;

namespace TeamJob.Services.Profile.Infrastructure.Mongo.Queries
{
    public class GetProfileHandler : IQueryHandler<GetProfile, UserProfileDetailsDto>
    {
        private readonly IMongoRepository<UserProfileDocument, Guid> _userProfileRepository;

        public GetProfileHandler(IMongoRepository<UserProfileDocument, Guid> userProfileRepository)
        {
            _userProfileRepository = userProfileRepository;
        }

        public async Task<UserProfileDetailsDto> HandleAsync(GetProfile query)
        {
            var profile = await _userProfileRepository.GetAsync(query.Id);

            return profile?.AsDetailsDto();
        }
    }
}

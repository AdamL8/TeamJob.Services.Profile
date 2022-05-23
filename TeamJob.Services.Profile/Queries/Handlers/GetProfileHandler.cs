using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;
using System;
using System.Threading.Tasks;
using TeamJob.Services.Profile.Domain;
using TeamJob.Services.Profile.DTO;

namespace TeamJob.Services.Profile.Queries.Handlers
{
    public class GetProfileHandler : IQueryHandler<GetProfile, ProfileDto>
    {
        private readonly IMongoRepository<UserProfile, string> _profileRepository;

        public GetProfileHandler(IMongoRepository<UserProfile, string> InProfileRepository)
        {
            _profileRepository = InProfileRepository;
        }

        public async Task<ProfileDto> HandleAsync(GetProfile InQuery)
        {
            var profile = await _profileRepository.GetAsync(InQuery.Id);

            return profile?.AsDto();
        }
    }
}

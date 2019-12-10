using Convey.CQRS.Events;
using Convey.MessageBrokers;
using Convey.Persistence.MongoDB;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TeamJob.Services.Profile.Domain;

namespace TeamJob.Services.Profile.Events.External.Handlers
{
    public class RemoveMemberHandler : IEventHandler<RemoveMember>
    {
        private readonly IMongoRepository<UserProfile, Guid> _profileRepository;
        private readonly IBusPublisher                       _busPublisher;
        private readonly ILogger<RemoveMemberHandler>        _logger;

        public RemoveMemberHandler(IMongoRepository<UserProfile, Guid> InProfileRepository,
                                   IBusPublisher                       InBusPublisher,
                                   ILogger<RemoveMemberHandler>        InLogger)
        {
            _profileRepository = InProfileRepository;
            _busPublisher      = InBusPublisher;
            _logger            = InLogger;
        }

        public async Task HandleAsync(RemoveMember InEvent)
        {
            var profile = await _profileRepository.GetAsync(InEvent.ProfileId);
            if (profile is null)
            {
                _logger.LogInformation($"Profile with ID : [{InEvent.ProfileId}] does not exist");
                return;
            }

            var teamAlreadyMemberOf = profile.Teams.Find(x => x.Id == InEvent.TeamId);
            if (teamAlreadyMemberOf is null)
            {
                _logger.LogWarning($"Profile with ID : [{InEvent.ProfileId}] is not part of the Team with ID : [{InEvent.TeamId}]");
                return;
            }

            if (teamAlreadyMemberOf.Status != TeamMemberStatus.Member)
            {
                _logger.LogError($"Profile with ID : [{InEvent.ProfileId}] is part of the Team with ID : [{InEvent.TeamId}] "
                        + $"as a [{teamAlreadyMemberOf.Status}] but was attempted to be removed as if he was a [{TeamMemberStatus.Member}]");
                return;
            }

            profile.Teams.Remove(teamAlreadyMemberOf);

            var updatedProfile = new UserProfile(InEvent.ProfileId,
                                                 profile.PersonalInformation,
                                                 profile.SatisfactionProfile,
                                                 profile.Role,
                                                 profile.Teams,
                                                 profile.CreatedAt);

            await _profileRepository.UpdateAsync(updatedProfile);
            _logger.LogInformation($"Profile with ID [{InEvent.ProfileId}] was REMOVED from the Team with ID : [{InEvent.TeamId}] as a [{TeamMemberStatus.Member}]");
        }
    }
}

using Convey.CQRS.Events;
using Convey.MessageBrokers;
using Convey.Persistence.MongoDB;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TeamJob.Services.Profile.Domain;

namespace TeamJob.Services.Profile.Events.External.Handlers
{
    public class AcceptCandidateHandler : IEventHandler<AcceptCandidate>
    {
        private readonly IMongoRepository<UserProfile, string> _profileRepository;
        private readonly IBusPublisher                       _busPublisher;
        private readonly ILogger<AcceptCandidateHandler>     _logger;

        public AcceptCandidateHandler(IMongoRepository<UserProfile, string> InProfileRepository,
                                      IBusPublisher                       InBusPublisher,
                                      ILogger<AcceptCandidateHandler>     InLogger)
        {
            _profileRepository = InProfileRepository;
            _busPublisher      = InBusPublisher;
            _logger            = InLogger;
        }

        public async Task HandleAsync(AcceptCandidate InEvent)
        {
            var profile = await _profileRepository.GetAsync(InEvent.ProfileId);
            if (profile is null)
            {
                _logger.LogError($"Candidate Profile with ID : [{InEvent.ProfileId}] does not exist");
                return;
            }

            var teamCandidateOf = profile.Teams.Find(x => x.Id == InEvent.TeamId);

            if (teamCandidateOf == null)
            {
                _logger.LogError($"Profile with ID : [{InEvent.ProfileId}] is not part of the Team with ID : [{InEvent.TeamId}] as a [{TeamMemberStatus.Candidate}]");
                return;
            }
            else if (teamCandidateOf.Status != TeamMemberStatus.Candidate)
            {
                _logger.LogError($"Profile with ID : [{InEvent.ProfileId}] the Team with ID : [{InEvent.TeamId}] is NOT a [{TeamMemberStatus.Candidate}]. Actual status : [{teamCandidateOf.Status}]");
                return;
            }

            var index = profile.Teams.IndexOf(teamCandidateOf);

            if (index != -1)
            {
                profile.Teams[index] = new Team(InEvent.TeamId, TeamMemberStatus.Member);
            }

            var updatedProfile = new UserProfile(InEvent.ProfileId,
                                                 profile.PersonalInformation,
                                                 profile.SatisfactionProfile,
                                                 profile.Role,
                                                 profile.Teams,
                                                 profile.CreatedAt);

            await _profileRepository.UpdateAsync(updatedProfile);
            _logger.LogInformation($"Profile with ID [{InEvent.ProfileId}] in Team with ID : [{InEvent.TeamId}] as been promoted to [{TeamMemberStatus.Member}] status");
        }
    }
}

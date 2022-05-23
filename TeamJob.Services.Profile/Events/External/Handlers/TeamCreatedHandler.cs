using Convey.CQRS.Events;
using Convey.MessageBrokers;
using Convey.Persistence.MongoDB;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamJob.Services.Profile.Domain;

namespace TeamJob.Services.Profile.Events.External.Handlers
{
    public class TeamCreatedHandler : IEventHandler<TeamCreated>
    {
        private readonly IMongoRepository<UserProfile, string> _profileRepository;
        private readonly IBusPublisher                       _busPublisher;
        private readonly ILogger<TeamCreatedHandler>         _logger;

        public TeamCreatedHandler(IMongoRepository<UserProfile, string> InProfileRepository,
                                      IBusPublisher                   InBusPublisher,
                                      ILogger<TeamCreatedHandler>     InLogger)
        {
            _profileRepository = InProfileRepository;
            _busPublisher      = InBusPublisher;
            _logger            = InLogger;
        }

        public async Task HandleAsync(TeamCreated InEvent)
        {
            var profile = await _profileRepository.GetAsync(InEvent.OwnerId);
            if (profile is null)
            {
                _logger.LogError($"Profile with ID : [{InEvent.OwnerId}] does not exist");
                return;
            }

            var teamId = InEvent.Id;

            var teamAlreadyPartOf = profile.Teams.SingleOrDefault(x => x.Id == teamId);
            if (teamAlreadyPartOf != null)
            {
                _logger.LogError($"Profile with ID : [{InEvent.OwnerId}] is already the part of the Team with ID : [{teamId}] with Status : [{teamAlreadyPartOf.Status}]");
                return;
            }

            profile.Teams.Add(new Team(teamId, TeamMemberStatus.Owner));

            await _profileRepository.UpdateAsync(profile);
            _logger.LogInformation($"Profile with ID [{profile.Id}] was ADDED to the Team with ID : [{teamId}] as an [{TeamMemberStatus.Owner}]");
        }
    }
}

using Convey.CQRS.Events;
using Convey.MessageBrokers;
using Convey.Persistence.MongoDB;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamJob.Services.Profile.Domain;

namespace TeamJob.Services.Profile.Events.External.Handlers
{
    public class TeamDeletedHandler : IEventHandler<TeamDeleted>
    {
        private readonly IMongoRepository<UserProfile, string> _profileRepository;
        private readonly IBusPublisher                       _busPublisher;
        private readonly ILogger<TeamDeletedHandler>         _logger;

        public TeamDeletedHandler(IMongoRepository<UserProfile, string> InProfileRepository,
                                  IBusPublisher                       InBusPublisher,
                                  ILogger<TeamDeletedHandler>         InLogger)
        {
            _profileRepository = InProfileRepository;
            _busPublisher = InBusPublisher;
            _logger = InLogger;
        }

        public async Task HandleAsync(TeamDeleted InEvent)
        {
            var profiles = _profileRepository.Collection?.AsQueryable();
            var teamId   = InEvent.Id;

            await profiles.ForEachAsync((profile) =>
            {
                var ret = profile.RemoveTeam(teamId);

                if (ret == false)
                { return;  }

                _profileRepository.UpdateAsync(profile);
                _logger.LogInformation($"Removed Team with ID : [{teamId}] from Profile with ID [{profile.Id}]'s list of teams");

            }).ConfigureAwait(false);
        }
    }
}

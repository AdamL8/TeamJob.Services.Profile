using Convey.CQRS.Commands;
using Convey.MessageBrokers;
using Convey.Persistence.MongoDB;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using TeamJob.Services.Profile.Domain;
using TeamJob.Services.Profile.Events;
using TeamJob.Services.Profile.Exceptions;

namespace TeamJob.Services.Profile.Commands.Handlers
{
    public class DeleteProfileHandler : ICommandHandler<DeleteProfile>
    {
        private readonly IMongoRepository<UserProfile, string> _profileRepository;
        private readonly IBusPublisher                       _busPublisher;
        private readonly ILogger<DeleteProfileHandler>       _logger;

        public DeleteProfileHandler(IMongoRepository<UserProfile, string> InProfileRepository,
                                    IBusPublisher                       InBusPublisher,
                                    ILogger<DeleteProfileHandler>       InLogger)
        {
            _profileRepository = InProfileRepository;
            _busPublisher      = InBusPublisher;
            _logger            = InLogger;
        }

        public async Task HandleAsync(DeleteProfile InCommand)
        {
            var profileId = InCommand.Id;

            var profile = await _profileRepository.GetAsync(profileId);
            if (profile is null)
            {
                _logger.LogError($"Cannot delete Profile with ID : [{profileId}] because it doesn't exist");
                await _busPublisher.PublishAsync(new ProfileDeletedRejected(profileId));
                
                throw new ProfileNotFoundException(profileId);
            }

            var associatedTeams = profile.Teams.Select(x => x.Id).ToList();

            await _profileRepository.DeleteAsync(profileId);

            _logger.LogInformation($"Profile with ID [{profileId}] was DELETED");
            await _busPublisher.PublishAsync(new ProfileDeleted(profileId, associatedTeams, profile.Role.ToString()));
        }
    }
}

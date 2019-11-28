using Convey.CQRS.Commands;
using Convey.MessageBrokers;
using Convey.Persistence.MongoDB;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using TeamJob.Services.Profile.Domain;
using TeamJob.Services.Profile.Events;

namespace TeamJob.Services.Profile.Commands.Handlers
{
    public class DeleteProfileHandler : ICommandHandler<DeleteProfile>
    {
        private readonly IMongoRepository<UserProfile, Guid> _profileRepository;
        private readonly IBusPublisher                       _busPublisher;
        private readonly ILogger<DeleteProfileHandler>       _logger;

        public DeleteProfileHandler(IMongoRepository<UserProfile, Guid> InProfileRepository,
                                    IBusPublisher                       InBusPublisher,
                                    ILogger<DeleteProfileHandler>       InLogger)
        {
            _profileRepository = InProfileRepository;
            _busPublisher      = InBusPublisher;
            _logger            = InLogger;
        }

        public async Task HandleAsync(DeleteProfile InCommand)
        {
            var profile = await _profileRepository.GetAsync(InCommand.Id);

            if (profile is null)
            {
                _logger.LogError($"Cannot delete Profile with ID : [{InCommand.Id}] because it doesn't exist");
                await _busPublisher.PublishAsync(new ProfileDeletedRejected(InCommand.Id));
                return;
            }

            var associatedTeams = profile.Teams.Select(x => x.Id).ToList();

            await _profileRepository.DeleteAsync(InCommand.Id);

            _logger.LogInformation($"Profile with ID [{InCommand.Id}] DELETED");
            await _busPublisher.PublishAsync(new ProfileDeleted(InCommand.Id, associatedTeams));
        }
    }
}

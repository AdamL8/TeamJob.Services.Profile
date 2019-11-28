using Convey.CQRS.Commands;
using Convey.MessageBrokers;
using Convey.Persistence.MongoDB;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TeamJob.Services.Profile.Domain;
using TeamJob.Services.Profile.Events;

namespace TeamJob.Services.Profile.Commands.Handlers
{
    public class UpdateProfileHandler : ICommandHandler<UpdateProfile>
    {
        private readonly IMongoRepository<UserProfile, Guid> _profileRepository;
        private readonly IBusPublisher                       _busPublisher;
        private readonly ILogger<UpdateProfileHandler>       _logger;

        public UpdateProfileHandler(IMongoRepository<UserProfile, Guid> InProfileRepository,
                                    IBusPublisher                       InBusPublisher,
                                    ILogger<UpdateProfileHandler>       InLogger)
        {
            _profileRepository = InProfileRepository;
            _busPublisher      = InBusPublisher;
            _logger            = InLogger;
        }

        public async Task HandleAsync(UpdateProfile InCommand)
        {
            var profile = await _profileRepository.GetAsync(InCommand.Id);

            if (profile is null)
            {
                _logger.LogInformation($"Cannot update Profile with ID : [{InCommand.Id}] because it doesn't exist");
                await _busPublisher.PublishAsync(new ProfileUpdatedRejected(InCommand.Id));
                return;
            }

            var updatedProfile = new UserProfile(InCommand.Id,
                                                 InCommand.PersonalInformation,
                                                 InCommand.SatisfactionProfile,
                                                 profile.Role,
                                                 InCommand.Teams,
                                                 profile.CreatedAt);

            await _profileRepository.UpdateAsync(updatedProfile);

            _logger.LogInformation($"Profile with ID : [{InCommand.Id}] UPDATED");
            await _busPublisher.PublishAsync(new ProfileUpdated(InCommand.Id));
        }
    }
}

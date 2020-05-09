using Convey.CQRS.Commands;
using Convey.MessageBrokers;
using Convey.Persistence.MongoDB;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TeamJob.Services.Profile.Domain;
using TeamJob.Services.Profile.Events;
using TeamJob.Services.Profile.Exceptions;

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
            var profileId = InCommand.Id;

            var profile = await _profileRepository.GetAsync(profileId);
            if (profile is null)
            {
                _logger.LogError($"Cannot update Profile with ID : [{profileId}] because it doesn't exist");
                await _busPublisher.PublishAsync(new ProfileUpdatedRejected(profileId));
                
                
                throw new ProfileNotFoundException(profileId);
            }

            var updatedProfile = new UserProfile(profileId,
                                                 InCommand.PersonalInformation,
                                                 InCommand.SatisfactionProfile,
                                                 profile.Role,
                                                 InCommand.Teams,
                                                 profile.CreatedAt);

            await _profileRepository.UpdateAsync(updatedProfile);

            _logger.LogInformation($"Profile with ID : [{profileId}] was UPDATED");
            await _busPublisher.PublishAsync(new ProfileUpdated(profileId, profile.Role.ToString()));
        }
    }
}

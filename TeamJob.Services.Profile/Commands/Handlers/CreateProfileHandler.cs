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
    public class CreateProfileHandler : ICommandHandler<CreateProfile>
    {
        private readonly IMongoRepository<UserProfile, string> _profileRepository;
        private readonly IBusPublisher                       _busPublisher;
        private readonly ILogger<CreateProfileHandler>       _logger;

        public CreateProfileHandler(IMongoRepository<UserProfile, string> InProfileRepository,
                                    IBusPublisher                       InBusPublisher,
                                    ILogger<CreateProfileHandler>       InLogger)
        {
            _profileRepository = InProfileRepository;
            _busPublisher      = InBusPublisher;
            _logger            = InLogger;
        }

        public async Task HandleAsync(CreateProfile InCommand)
        {
            var profileId = InCommand.Id;

            var profile = await _profileRepository.GetAsync(profileId);
            if (profile != null)
            {
                _logger.LogError($"Cannot create new profile because a with ID : [{profileId}] already exists");
                await _busPublisher.PublishAsync(new ProfileCreatedRejected(profileId, InCommand.Role.ToString()));

                throw new ProfileAlreadyExistsException(profileId);
            }

            var profileRole = Enum.Parse<Role>(InCommand.Role);

            await _profileRepository.AddAsync(new UserProfile(profileId,
                                                              InCommand.PersonalInformation,
                                                              InCommand.SatisfactionProfile,
                                                              profileRole));

            _logger.LogInformation($"New profile with ID [{profileId}] and Role [{InCommand.Role}] was CREATED");
            await _busPublisher.PublishAsync(new ProfileCreated(profileId, InCommand.Role.ToString()));
        }
    }
}

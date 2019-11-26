﻿using Convey.CQRS.Commands;
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
        private readonly IMongoRepository<UserProfile, Guid> _profileRepository;
        private readonly IBusPublisher                       _busPublisher;
        private readonly ILogger<CreateProfileHandler>       _logger;

        public CreateProfileHandler(IMongoRepository<UserProfile, Guid> InProfileRepository,
                                    IBusPublisher                       InBusPublisher,
                                    ILogger<CreateProfileHandler>       InLogger)
        {
            _profileRepository = InProfileRepository;
            _busPublisher      = InBusPublisher;
            _logger            = InLogger;
        }

        public async Task HandleAsync(CreateProfile InCommand)
        {
            var profile = await _profileRepository.GetAsync(x => x.Id == InCommand.ProfileId);

            if (profile != null)
            {
                _logger.LogInformation($"Cannot create new profile because one with ID : [{InCommand.ProfileId}] already exists");
                await _busPublisher.PublishAsync(new ProfileCreatedRejected(InCommand.ProfileId, InCommand.Role.ToString()));

                throw new TeamJobException("Codes.ProfileAlreadyExists",
                    $"A profile with Id: '{InCommand.ProfileId}' already exist.");
            }

            var profileRole = Enum.Parse<Role>(InCommand.Role);

            await _profileRepository.AddAsync(new UserProfile(InCommand.ProfileId,
                                                              InCommand.PersonalInformation,
                                                              InCommand.SatisfactionProfile,
                                                              profileRole));

            _logger.LogInformation($"New profile with ID [{InCommand.ProfileId}] and Role [{InCommand.Role}] CREATED");
            await _busPublisher.PublishAsync(new ProfileCreated(InCommand.ProfileId, InCommand.Role.ToString()));
        }
    }
}
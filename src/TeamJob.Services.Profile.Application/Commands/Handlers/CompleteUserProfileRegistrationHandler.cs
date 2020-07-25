using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Convey.CQRS.Commands;
using TeamJob.Services.Profile.Application.Exceptions;
using TeamJob.Services.Profile.Application.Services;
using TeamJob.Services.Profile.Core.Entities;
using TeamJob.Services.Profile.Core.Exceptions;
using TeamJob.Services.Profile.Core.Repositories;

namespace TeamJob.Services.Profile.Application.Commands.Handlers
{
    public class CompleteUserProfileRegistrationHandler : ICommandHandler<CompleteUserProfileRegistration>
    {
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IEventMapper           _eventMapper;
        private readonly IMessageBroker         _messageBroker;

        public CompleteUserProfileRegistrationHandler(IUserProfileRepository userProfileRepository,
                                                      IEventMapper           eventMapper,
                                                      IMessageBroker         messageBroker)
        {
            _userProfileRepository = userProfileRepository;
            _eventMapper           = eventMapper;
            _messageBroker         = messageBroker;
        }

        public async Task HandleAsync(CompleteUserProfileRegistration command)
        {
            var userProfile = await _userProfileRepository.GetAsync(command.Id);
            if (userProfile is null)
            {
                throw new UserProfileNotFoundException(command.Id);
            }

            if (userProfile.State is State.Valid)
            {
                throw new UserProfileAlreadyRegisteredException(command.Id);
            }

            if (Enum.TryParse<Role>(command.Role, true, out var role) == false)
            {
                throw new CannotSetUserProfileRoleException(userProfile.Id, command.Role);
            }

            userProfile.CompleteRegistration(command.PersonalInformation, command.SatisfactionProfile, role);
            await _userProfileRepository.UpdateAsync(userProfile);

            var events = _eventMapper.MapAll(userProfile.Events);
            await _messageBroker.PublishAsync(events.ToArray());
        }
    }
}

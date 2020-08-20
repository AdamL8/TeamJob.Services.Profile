using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Convey.CQRS.Commands;
using Convey.MessageBrokers;
using Microsoft.Extensions.Logging;
using TeamJob.Services.Profile.Application.Events;
using TeamJob.Services.Profile.Application.Exceptions;
using TeamJob.Services.Profile.Application.Services;
using TeamJob.Services.Profile.Core.Entities;
using TeamJob.Services.Profile.Core.Exceptions;
using TeamJob.Services.Profile.Core.Repositories;

namespace TeamJob.Services.Profile.Application.Commands.Handlers
{
    public class DeleteProfileHandler : ICommandHandler<DeleteProfile>
    {
        private readonly IUserProfileRepository        _userProfileRepository;
        private readonly IMessageBroker                _messageBroker;

        public DeleteProfileHandler(IUserProfileRepository        userProfileRepository,
                                    IMessageBroker                messageBroker)
        {
            _userProfileRepository = userProfileRepository;
            _messageBroker         = messageBroker;
        }

        public async Task HandleAsync(DeleteProfile command)
        {
            var userProfile = await _userProfileRepository.GetAsync(command.Id);
            if (userProfile is null)
            {
                throw new UserProfileNotFoundException(command.Id);
            }

            var associatedTeams = userProfile.Teams.Select(x => x.Id).ToList();
            
            await _userProfileRepository.DeleteAsync(userProfile.Id);
            await _messageBroker.PublishAsync(new ProfileDeleted(userProfile.Id, associatedTeams, userProfile.Role.ToString()));
        }
    }
}

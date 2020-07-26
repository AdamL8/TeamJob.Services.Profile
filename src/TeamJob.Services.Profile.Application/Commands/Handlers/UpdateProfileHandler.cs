using System.Threading.Tasks;
using Convey.CQRS.Commands;
using TeamJob.Services.Profile.Application.Events;
using TeamJob.Services.Profile.Application.Exceptions;
using TeamJob.Services.Profile.Application.Services;
using TeamJob.Services.Profile.Core.Entities;
using TeamJob.Services.Profile.Core.Exceptions;
using TeamJob.Services.Profile.Core.Repositories;

namespace TeamJob.Services.Profile.Application.Commands.Handlers
{
    public class UpdateProfileHandler : ICommandHandler<UpdateProfile>
    {
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IMessageBroker         _messageBroker;

        public UpdateProfileHandler(IUserProfileRepository userProfileRepository,
                                    IMessageBroker         messageBroker)
        {
            _userProfileRepository = userProfileRepository;
            _messageBroker         = messageBroker;
        }

        public async Task HandleAsync(UpdateProfile command)
        {
            var userProfile = await _userProfileRepository.GetAsync(command.Id);
            if (userProfile is null)
            {
                throw new  UserProfileNotFoundException(command.Id);
            }

            if (userProfile.State is State.Incomplete)
            {
                throw new IncompleteUserProfileException(command.Id);
            }

            var updatedProfile = new UserProfile(command.Id,
                                                 userProfile.Email,
                                                 command.PersonalInformation,
                                                 command.SatisfactionProfile,
                                                 userProfile.Role,
                                                 userProfile.State,
                                                 command.Teams,
                                                 userProfile.CreatedAt);

            await _userProfileRepository.UpdateAsync(updatedProfile);
            await _messageBroker.PublishAsync(new ProfileUpdated(command.Id, userProfile.Role.ToString()));
        }
    }
}

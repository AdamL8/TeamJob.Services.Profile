using System.Threading.Tasks;
using Convey.CQRS.Events;
using Microsoft.Extensions.Logging;
using TeamJob.Services.Profile.Application.Exceptions;
using TeamJob.Services.Profile.Application.Services;
using TeamJob.Services.Profile.Core.Entities;
using TeamJob.Services.Profile.Core.Exceptions;
using TeamJob.Services.Profile.Core.Repositories;

namespace TeamJob.Services.Profile.Application.Events.External.Handlers
{
    public class RegisteredHandler : IEventHandler<Registered>
    {
        private const string RequiredRole = "user";

        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IDateTimeProvider      _dateTimeProvider;

        public RegisteredHandler(IUserProfileRepository userProfileRepository,
                                 IDateTimeProvider      dateTimeProvider)
        {
            _userProfileRepository = userProfileRepository;
            _dateTimeProvider      = dateTimeProvider;
        }

        public async Task HandleAsync(Registered @event)
        {
            if (@event.Role != RequiredRole)
            {
                throw new InvalidRoleException(@event.Id, @event.Role, RequiredRole);
            }

            var profile = await _userProfileRepository.GetAsync(@event.Id);
            if (profile is { })
            {
                throw new UserProfileAlreadyCreatedException(profile.Id);
            }

            profile = new UserProfile(@event.Id, @event.Email, _dateTimeProvider.Now);
            await _userProfileRepository.AddAsync(profile);
        }
    }
}

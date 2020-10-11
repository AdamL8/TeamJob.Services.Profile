using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Events;
using TeamJob.Services.Profile.Application.Exceptions;
using TeamJob.Services.Profile.Application.Services;
using TeamJob.Services.Profile.Core.Entities;
using TeamJob.Services.Profile.Core.Repositories;

namespace TeamJob.Services.Profile.Application.Events.External.Handlers
{
    public class RemoveMemberHandler : IEventHandler<RemoveMember>
    {
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IDateTimeProvider      _dateTimeProvider;

        public RemoveMemberHandler(IUserProfileRepository userProfileRepository,
                                   IDateTimeProvider      dateTimeProvider)
        {
            _userProfileRepository = userProfileRepository;
            _dateTimeProvider      = dateTimeProvider;
        }

        public async Task HandleAsync(RemoveMember @event)
        {
            var profile = await _userProfileRepository.GetAsync(@event.ProfileId);
            if (profile is null)
            {
                throw new UserProfileNotFoundException(@event.ProfileId);
            }

            var teamAlreadyMemberOf = profile.Teams.SingleOrDefault(x => x.Id == @event.TeamId);
            if (teamAlreadyMemberOf is null)
            {
                throw new UserProfileNotPartOfTeamException(@event.ProfileId, @event.TeamId);
            }

            if (teamAlreadyMemberOf.Status != TeamMemberStatus.Member)
            {
                throw new IncorrectTeamMemberStatusForUserProfileException(profile.Id, @event.TeamId, teamAlreadyMemberOf.Status, TeamMemberStatus.Member);
            }

            profile.RemoveTeam(teamAlreadyMemberOf);

            await _userProfileRepository.UpdateAsync(profile);
        }
    }
}

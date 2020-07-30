using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Events;
using TeamJob.Services.Profile.Application.Exceptions;
using TeamJob.Services.Profile.Application.Services;
using TeamJob.Services.Profile.Core.Entities;
using TeamJob.Services.Profile.Core.Repositories;

namespace TeamJob.Services.Profile.Application.Events.External.Handlers
{
    public class AcceptCandidateHandlerHandler : IEventHandler<AcceptCandidate>
    {
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IDateTimeProvider      _dateTimeProvider;

        public AcceptCandidateHandlerHandler(IUserProfileRepository userProfileRepository,
                                             IDateTimeProvider      dateTimeProvider)
        {
            _userProfileRepository = userProfileRepository;
            _dateTimeProvider      = dateTimeProvider;
        }

        public async Task HandleAsync(AcceptCandidate @event)
        {
            var profile = await _userProfileRepository.GetAsync(@event.ProfileId);
            if (profile is null)
            {
                throw new UserProfileNotFoundException(@event.ProfileId);
            }

            var teamAlreadyMemberOf = profile.Teams.SingleOrDefault(x => x.Id == @event.TeamId);
            if (teamAlreadyMemberOf is null)
            {
                throw new UserProfileNotPartOfTeamException(profile.Id, @event.TeamId);
            }

            if (teamAlreadyMemberOf.Status != TeamMemberStatus.Candidate)
            {
                throw new IncorrectTeamMemberStatusForUserProfileException(profile.Id, @event.TeamId, teamAlreadyMemberOf.Status, TeamMemberStatus.Candidate);
            }

            teamAlreadyMemberOf.ChangeStatus(TeamMemberStatus.Member);

            await _userProfileRepository.UpdateAsync(profile);
        }
    }
}

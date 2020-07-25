using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Convey.CQRS.Events;
using Microsoft.Extensions.Logging;
using TeamJob.Services.Profile.Application.Exceptions;
using TeamJob.Services.Profile.Application.Services;
using TeamJob.Services.Profile.Core.Entities;
using TeamJob.Services.Profile.Core.Repositories;

namespace TeamJob.Services.Profile.Application.Events.External.Handlers
{
    public class TeamCreatedHandler : IEventHandler<TeamCreated>
    {
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IDateTimeProvider      _dateTimeProvider;

        public TeamCreatedHandler(IUserProfileRepository userProfileRepository,
                                  IDateTimeProvider      dateTimeProvider)
        {
            _userProfileRepository = userProfileRepository;
            _dateTimeProvider      = dateTimeProvider;
        }

        public async Task HandleAsync(TeamCreated @event)
        {
            var profile = await _userProfileRepository.GetAsync(@event.OwnerId);
            if (profile is null)
            {
                throw new UserProfileNotFoundException(@event.OwnerId);
            }

            var teamAlreadyMemberOf = profile.Teams.SingleOrDefault(x => x.Id == @event.Id);
            if (teamAlreadyMemberOf is { })
            {
                throw new UserProfileAlreadyInTeamException(@event.OwnerId, @event.Id, teamAlreadyMemberOf.Status);
            }

            profile.AddTeam(new Team(@event.Id, @event.Name, TeamMemberStatus.Owner));

            await _userProfileRepository.UpdateAsync(profile);
        }
    }
}

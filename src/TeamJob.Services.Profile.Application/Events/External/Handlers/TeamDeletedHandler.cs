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
    public class TeamDeletedHandler : IEventHandler<TeamDeleted>
    {
        private readonly IUserProfileRepository      _userProfileRepository;
        private readonly IDateTimeProvider           _dateTimeProvider;
        private readonly ILogger<TeamDeletedHandler> _logger;

        public TeamDeletedHandler(IUserProfileRepository      userProfileRepository,
                                  IDateTimeProvider           dateTimeProvider,
                                  ILogger<TeamDeletedHandler> logger)
        {
            _userProfileRepository = userProfileRepository;
            _dateTimeProvider      = dateTimeProvider;
            _logger                = logger;
        }

        public async Task HandleAsync(TeamDeleted @event)
        {
            var profiles = await _userProfileRepository.GetAllAsync();

            profiles.ForEach((profile) =>
            {
                var ret = profile.RemoveTeamById(@event.Id);

                if (ret == false)
                { return; }

                _userProfileRepository.UpdateAsync(profile);
                _logger.LogInformation($"Removed Team with ID : [{@event.Id}] from Profile with ID [{profile.Id}]'s list of teams");
            });
        }
    }
}

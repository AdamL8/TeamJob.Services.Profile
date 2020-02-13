﻿using Convey.CQRS.Events;
using Convey.MessageBrokers;
using Convey.Persistence.MongoDB;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TeamJob.Services.Profile.Domain;

namespace TeamJob.Services.Profile.Events.External.Handlers
{
    public class AddCandidateHandler : IEventHandler<AddCandidate>
    {
        private readonly IMongoRepository<UserProfile, Guid> _profileRepository;
        private readonly IBusPublisher                       _busPublisher;
        private readonly ILogger<AddCandidateHandler>        _logger;

        public AddCandidateHandler(IMongoRepository<UserProfile, Guid> InProfileRepository,
                                   IBusPublisher                       InBusPublisher,
                                   ILogger<AddCandidateHandler>        InLogger)
        {
            _profileRepository = InProfileRepository;
            _busPublisher      = InBusPublisher;
            _logger            = InLogger;
        }

        public async Task HandleAsync(AddCandidate InEvent)
        {
            var profile = await _profileRepository.GetAsync(InEvent.ProfileId);
            if (profile is null)
            {
                _logger.LogInformation($"Profile with ID : [{InEvent.ProfileId}] does not exist");
                return;
            }

            var teamAlreadyMemberOf = profile.Teams.Find(x => x.Id == InEvent.TeamId);
            if (teamAlreadyMemberOf != null)
            {
                _logger.LogInformation($"Profile with ID : [{InEvent.ProfileId}] is already part of the Team with ID : [{InEvent.TeamId}] with Status : [{teamAlreadyMemberOf.Status}]");
                return;
            }

            profile.Teams.Add(new Team(InEvent.TeamId, TeamMemberStatus.Candidate));

            var updatedProfile = new UserProfile(InEvent.ProfileId,
                                                 profile.PersonalInformation,
                                                 profile.SatisfactionProfile,
                                                 profile.Role,
                                                 profile.Teams,
                                                 profile.CreatedAt);

            await _profileRepository.UpdateAsync(updatedProfile);
            _logger.LogInformation($"Profile with ID [{InEvent.ProfileId}] was ADDED to the Team with ID : [{InEvent.TeamId}] as a [{TeamMemberStatus.Candidate}]");
        }
    }
}
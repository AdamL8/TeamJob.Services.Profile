using System;
using System.Collections.Generic;
using System.Text;
using TeamJob.Services.Profile.Core.Entities;

namespace TeamJob.Services.Profile.Application.Exceptions
{
    public class UserProfileAlreadyInTeamException : AppException
    {
        public override string Code { get; } = "service.profile.exception.user_profile_already_in_team";
        public Guid Id                        { get; }
        public Guid TeamId                    { get; }
        public TeamMemberStatus CurrentStatus { get; }

        public UserProfileAlreadyInTeamException(Guid id, Guid teamId, TeamMemberStatus status)
            : base($"User profile with id: {id} is already part of the team with id: {teamId} as a {status}.")
        {
            Id            = id;
            TeamId        = teamId;
            CurrentStatus = status;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace TeamJob.Services.Profile.Application.Exceptions
{
    public class UserProfileNotPartOfTeamException : AppException
    {
        public override string Code { get; } = "service.profile.exception.user_profile_not_part_of_team";
        public Guid Id     { get; }
        public Guid TeamId { get; }

        public UserProfileNotPartOfTeamException(Guid id, Guid teamId)
            : base($"User profile with id: {id} is not part of the team with id: {teamId}.")
        {
            Id     = id;
            TeamId = teamId;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace TeamJob.Services.Profile.Application.Exceptions
{
    public class UserProfileNotPartOfTeamException : AppException
    {
        public override string Code { get; } = "service.profile.exception.user_profile_not_part_of_team";
        public string Id     { get; }
        public string TeamId { get; }

        public UserProfileNotPartOfTeamException(string id, string teamId)
            : base($"User profile with id: {id} is not part of the team with id: {teamId}.")
        {
            Id     = id;
            TeamId = teamId;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using TeamJob.Services.Profile.Core.Entities;

namespace TeamJob.Services.Profile.Application.Exceptions
{
    public class IncorrectTeamMemberStatusForUserProfileException : AppException
    {
        public override string Code { get; } = "service.profile.exception.incorrect_team_member_status_for_user_profile";
        public string Id                         { get; }
        public string TeamId                     { get; }
        public TeamMemberStatus ActualStatus   { get; }
        public TeamMemberStatus ExpectedStatus { get; }

        public IncorrectTeamMemberStatusForUserProfileException(string             id,
                                                                string             teamId,
                                                                TeamMemberStatus actualStatus,
                                                                TeamMemberStatus expectedStatus)
            : base($"Profile with id: {id} in team with id : {teamId} is NOT a {expectedStatus}. Actual status : {actualStatus}")
        {
            Id             = id;
            TeamId         = teamId;
            ActualStatus   = actualStatus;
            ExpectedStatus = expectedStatus;
        }
    }
}

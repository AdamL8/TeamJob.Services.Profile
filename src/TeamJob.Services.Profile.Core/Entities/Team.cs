using System;

namespace TeamJob.Services.Profile.Core.Entities
{
    public class Team
    {
        public Guid Id                 { get; private set; }
        public string Name             { get; private set; }
        public TeamMemberStatus Status { get; private set; }

        public Team(Guid id, string name, TeamMemberStatus status)
        {
            Id     = id;
            Name   = name;
            Status = status;
        }

        public void ChangeStatus(TeamMemberStatus status)
        {
            Status = status;
        }
    }
}
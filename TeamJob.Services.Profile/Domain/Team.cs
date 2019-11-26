using Newtonsoft.Json;
using System;

namespace TeamJob.Services.Profile.Domain
{
    public class Team
    {
        public Guid Id                 { get; private set; }
        public TeamMemberStatus Status { get; private set; }

        [JsonConstructor]
        public Team(Guid id, TeamMemberStatus status)
        {
            Id = id;
            Status = status;
        }
    }
}

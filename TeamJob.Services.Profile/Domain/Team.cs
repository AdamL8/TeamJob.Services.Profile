using Newtonsoft.Json;
using System;

namespace TeamJob.Services.Profile.Domain
{
    public class Team
    {
        public string Id                 { get; private set; }
        public TeamMemberStatus Status { get; private set; }

        [JsonConstructor]
        public Team(string id, TeamMemberStatus status)
        {
            Id = id;
            Status = status;
        }
    }
}

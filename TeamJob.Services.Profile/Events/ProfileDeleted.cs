using Convey.CQRS.Events;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace TeamJob.Services.Profile.Events
{
    public class ProfileDeleted :IEvent
    {
        public Guid Id            { get; }
        public List<Guid> TeamIds { get; }
        public string Role        { get; }


        [JsonConstructor]
        public ProfileDeleted(Guid id, List<Guid> teamIds, string role)
        {
            Id      = id;
            TeamIds = teamIds;
            Role    = role;
        }
    }
}

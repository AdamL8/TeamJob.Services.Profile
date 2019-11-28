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

        [JsonConstructor]
        public ProfileDeleted(Guid id, List<Guid> teamIds)
        {
            Id      = id;
            TeamIds = teamIds;
        }
    }
}

using Convey.CQRS.Events;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace TeamJob.Services.Profile.Events
{
    public class ProfileDeleted :IEvent
    {
        public Guid ProfileId { get; }
        public List<Guid> Teams { get; }

        [JsonConstructor]
        public ProfileDeleted(Guid profileId, List<Guid> teams)
        {
            ProfileId = profileId;
            Teams     = teams;
        }
    }
}

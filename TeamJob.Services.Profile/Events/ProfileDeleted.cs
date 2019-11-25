using Convey.CQRS.Events;
using Newtonsoft.Json;
using System;

namespace TeamJob.Services.Profile.Events
{
    public class ProfileDeleted :IEvent
    {
        public Guid ProfileId { get; }

        [JsonConstructor]
        public ProfileDeleted(Guid profileId)
        {
            ProfileId = profileId;
        }
    }
}

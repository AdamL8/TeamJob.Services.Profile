using Convey.CQRS.Events;
using Newtonsoft.Json;
using System;

namespace TeamJob.Services.Profile.Events
{
    public class ProfileUpdated : IEvent
    {
        public Guid ProfileId { get; }

        [JsonConstructor]
        public ProfileUpdated(Guid profileId)
        {
            ProfileId = profileId;
        }
    }
}

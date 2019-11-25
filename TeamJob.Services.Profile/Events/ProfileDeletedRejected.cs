using Convey.CQRS.Events;
using Newtonsoft.Json;
using System;

namespace TeamJob.Services.Profile.Events
{
    public class ProfileDeletedRejected : IEvent
    {
        public Guid ProfileId { get; }

        [JsonConstructor]
        public ProfileDeletedRejected(Guid profileId)
        {
            ProfileId = profileId;
        }
    }
}

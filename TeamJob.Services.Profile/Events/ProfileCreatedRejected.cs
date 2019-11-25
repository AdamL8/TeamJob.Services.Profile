using Convey.CQRS.Events;
using Newtonsoft.Json;
using System;

namespace TeamJob.Services.Profile.Events
{
    public class ProfileCreatedRejected : IEvent
    {
        public Guid ProfileId { get; }
        public string Role    { get; }

        [JsonConstructor]
        public ProfileCreatedRejected(Guid profileId, string role)
        {
            ProfileId = profileId;
            Role      = role;
        }
    }
}

using Convey.CQRS.Events;
using Newtonsoft.Json;
using System;

namespace TeamJob.Services.Profile.Events
{
    public class ProfileCreated : IEvent
    {
        public Guid ProfileId { get; }
        public string Role    { get; }

        [JsonConstructor]
        public ProfileCreated(Guid profileId, string role)
        {
            ProfileId = profileId;
            Role      = role;
        }
    }
}

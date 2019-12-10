using Convey.CQRS.Events;
using Newtonsoft.Json;
using System;

namespace TeamJob.Services.Profile.Events
{
    public class ProfileUpdated : IEvent
    {
        public Guid Id     { get; }
        public string Role { get; }

        [JsonConstructor]
        public ProfileUpdated(Guid id, string role)
        {
            Id   = id;
            Role = role;
        }
    }
}

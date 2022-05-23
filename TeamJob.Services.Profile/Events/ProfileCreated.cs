using Convey.CQRS.Events;
using Newtonsoft.Json;
using System;

namespace TeamJob.Services.Profile.Events
{
    public class ProfileCreated : IEvent
    {
        public string Id     { get; }
        public string Role { get; }

        [JsonConstructor]
        public ProfileCreated(string id, string role)
        {
            Id   = id;
            Role = role;
        }
    }
}

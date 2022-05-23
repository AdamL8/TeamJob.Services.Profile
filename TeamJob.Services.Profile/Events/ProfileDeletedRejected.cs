using Convey.CQRS.Events;
using Newtonsoft.Json;
using System;

namespace TeamJob.Services.Profile.Events
{
    public class ProfileDeletedRejected : IEvent
    {
        public string Id { get; }

        [JsonConstructor]
        public ProfileDeletedRejected(string id)
        {
            Id = id;
        }
    }
}

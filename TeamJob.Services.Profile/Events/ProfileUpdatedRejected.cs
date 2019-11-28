using Convey.CQRS.Events;
using Newtonsoft.Json;
using System;

namespace TeamJob.Services.Profile.Events
{
    public class ProfileUpdatedRejected : IEvent
    {
        public Guid Id { get; }

        [JsonConstructor]
        public ProfileUpdatedRejected(Guid id)
        {
            Id = id;
        }
    }
}

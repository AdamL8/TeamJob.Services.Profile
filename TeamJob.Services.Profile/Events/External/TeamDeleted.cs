using Convey.CQRS.Events;
using Convey.MessageBrokers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamJob.Services.Profile.Events.External
{
    [Message(exchange: "team", external: true)]
    public class TeamDeleted : IEvent
    {
        public Guid Id      { get; }
        public Guid OwnerId { get; }

        [JsonConstructor]
        public TeamDeleted(Guid id, Guid ownerId)
        {
            Id      = id;
            OwnerId = ownerId;
        }
    }
}

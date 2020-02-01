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
    public class TeamCreated : IEvent
                            {
        public Guid Id      { get; }
        public Guid OwnerId { get; }
        public string Name  { get; }

        [JsonConstructor]
        public TeamCreated(Guid id, Guid ownerId, string name)
        {
            Id      = id;
            OwnerId = ownerId;
            Name    = name;
        }
    }
}

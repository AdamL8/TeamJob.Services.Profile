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
        public string Id      { get; }
        public string OwnerId { get; }
        public string Name  { get; }

        [JsonConstructor]
        public TeamCreated(string id, string ownerId, string name)
        {
            Id      = id;
            OwnerId = ownerId;
            Name    = name;
        }
    }
}

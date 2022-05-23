using System;
using System.Collections.Generic;
using System.Text;
using Convey.CQRS.Events;
using Convey.MessageBrokers;
using Newtonsoft.Json;

namespace TeamJob.Services.Profile.Application.Events.External
{
    [Message(exchange: "team", external: true)]
    public class TeamDeleted : IEvent
    {
        public string Id      { get; }
        public string OwnerId { get; }

        [JsonConstructor]
        public TeamDeleted(string id, string ownerId)
        {
            Id      = id;
            OwnerId = ownerId;
        }
    }
}

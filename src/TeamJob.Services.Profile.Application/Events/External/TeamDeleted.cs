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

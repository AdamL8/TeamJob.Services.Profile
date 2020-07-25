using System;
using System.Collections.Generic;
using System.Text;
using Convey.CQRS.Events;
using Convey.MessageBrokers;
using Newtonsoft.Json;

namespace TeamJob.Services.Profile.Application.Events.External
{
    [Message(exchange: "team", external: true)]
    public class RemoveCandidate : IEvent
    {
        public Guid ProfileId { get; }
        public Guid TeamId    { get; }

        [JsonConstructor]
        public RemoveCandidate(Guid profileId, Guid teamId)
        {
            ProfileId = profileId;
            TeamId    = teamId;
        }
    }
}

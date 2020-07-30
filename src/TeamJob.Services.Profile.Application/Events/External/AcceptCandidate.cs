using Convey.CQRS.Events;
using Convey.MessageBrokers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TeamJob.Services.Profile.Application.Events.External
{
    [Message(exchange: "team", external: true)]
    public class AcceptCandidate : IEvent
    {
        public Guid ProfileId { get; }
        public Guid TeamId    { get; }

        [JsonConstructor]
        public AcceptCandidate(Guid profileId, Guid teamId)
        {
            ProfileId = profileId;
            TeamId    = teamId;
        }
    }
}

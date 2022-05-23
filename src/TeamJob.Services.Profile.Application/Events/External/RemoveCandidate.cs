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
        public string ProfileId { get; }
        public string TeamId    { get; }

        [JsonConstructor]
        public RemoveCandidate(string profileId, string teamId)
        {
            ProfileId = profileId;
            TeamId    = teamId;
        }
    }
}

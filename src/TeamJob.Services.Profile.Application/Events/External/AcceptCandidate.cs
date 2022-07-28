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
        public string ProfileId { get; }
        public string TeamId    { get; }

        [JsonConstructor]
        public AcceptCandidate(string profileId, string teamId)
        {
            ProfileId = profileId;
            TeamId    = teamId;
        }
    }
}

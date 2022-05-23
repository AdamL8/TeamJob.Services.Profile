using Convey.CQRS.Events;
using Convey.MessageBrokers;
using Newtonsoft.Json;
using System;

namespace TeamJob.Services.Profile.Events.External
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

using Convey.CQRS.Events;
using Convey.MessageBrokers;
using Newtonsoft.Json;
using System;

namespace TeamJob.Services.Profile.Events.External
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

using System;
using System.Collections.Generic;
using System.Text;
using Convey.CQRS.Events;
using Convey.MessageBrokers;
using Newtonsoft.Json;

namespace TeamJob.Services.Profile.Application.Events.External
{
    [Message(exchange: "team", external: true)]
    public class AddCandidate : IEvent
    {
        public string ProfileId  { get; }
        public string TeamId     { get; }
        public string TeamName { get; }

        [JsonConstructor]
        public AddCandidate(string profileId, string teamId, string teamName)
        {
            ProfileId = profileId;
            TeamId    = teamId;
            TeamName = teamName;
        }
    }
}

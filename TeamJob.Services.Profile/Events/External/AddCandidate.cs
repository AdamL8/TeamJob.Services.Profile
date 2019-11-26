﻿using Convey.CQRS.Events;
using Convey.MessageBrokers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamJob.Services.Profile.Events.External
{
    [Message(exchange: "team", external: true)]
    public class AddCandidate : IEvent
    {
        public Guid ProfileId { get; }
        public Guid TeamId    { get; }

        [JsonConstructor]
        public AddCandidate(Guid profileId, Guid teamId)
        {
            ProfileId = profileId;
            TeamId    = teamId;
        }
    }
}

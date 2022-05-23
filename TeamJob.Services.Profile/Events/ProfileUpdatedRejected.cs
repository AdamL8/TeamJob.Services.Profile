﻿using Convey.CQRS.Events;
using Newtonsoft.Json;
using System;

namespace TeamJob.Services.Profile.Events
{
    public class ProfileUpdatedRejected : IEvent
    {
        public string Id { get; }

        [JsonConstructor]
        public ProfileUpdatedRejected(string id)
        {
            Id = id;
        }
    }
}

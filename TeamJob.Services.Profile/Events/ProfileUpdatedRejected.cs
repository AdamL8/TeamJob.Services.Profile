﻿using Convey.CQRS.Events;
using Newtonsoft.Json;
using System;

namespace TeamJob.Services.Profile.Events
{
    public class ProfileUpdatedRejected : IEvent
    {
        public Guid ProfileId { get; }

        [JsonConstructor]
        public ProfileUpdatedRejected(Guid profileId)
        {
            ProfileId = profileId;
        }
    }
}
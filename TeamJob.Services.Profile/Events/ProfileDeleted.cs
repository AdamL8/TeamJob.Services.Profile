using Convey.CQRS.Events;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace TeamJob.Services.Profile.Events
{
    public class ProfileDeleted :IEvent
    {
        public string Id            { get; }
        public List<string> TeamIds { get; }
        public string Role        { get; }


        [JsonConstructor]
        public ProfileDeleted(string id, List<string> teamIds, string role)
        {
            Id      = id;
            TeamIds = teamIds;
            Role    = role;
        }
    }
}

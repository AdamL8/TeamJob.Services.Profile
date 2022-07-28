using System;
using System.Collections.Generic;
using System.Text;
using Convey.CQRS.Events;

namespace TeamJob.Services.Profile.Application.Events
{
    public class ProfileDeleted : IEvent
    {
        public string Id            { get; }
        public List<string> TeamIds { get; }
        public string Role        { get; }

        public ProfileDeleted(string id, List<string> teamIds, string role)
        {
            Id      = id;
            TeamIds = teamIds;
            Role    = role;
        }
    }
}

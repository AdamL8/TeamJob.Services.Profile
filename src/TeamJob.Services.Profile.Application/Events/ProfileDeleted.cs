using System;
using System.Collections.Generic;
using System.Text;
using Convey.CQRS.Events;

namespace TeamJob.Services.Profile.Application.Events
{
    public class ProfileDeleted : IEvent
    {
        public Guid Id            { get; }
        public List<Guid> TeamIds { get; }
        public string Role        { get; }

        public ProfileDeleted(Guid id, List<Guid> teamIds, string role)
        {
            Id      = id;
            TeamIds = teamIds;
            Role    = role;
        }
    }
}

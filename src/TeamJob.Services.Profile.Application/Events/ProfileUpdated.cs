using System;
using System.Collections.Generic;
using System.Text;
using Convey.CQRS.Events;

namespace TeamJob.Services.Profile.Application.Events
{
    public class ProfileUpdated : IEvent
    {
        public string Id     { get; }
        public string Role { get; }

        public ProfileUpdated(string id, string role)
        {
            Id   = id;
            Role = role;
        }
    }
}

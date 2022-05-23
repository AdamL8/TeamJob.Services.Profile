using System;
using Convey.CQRS.Events;

namespace TeamJob.Services.Profile.Application.Events
{
    public class ProfileCreated : IEvent
    {
        public string Id     { get; }
        public string Role { get; }

        public ProfileCreated(string id, string role)
        {
            Id   = id;
            Role = role;

        }
    }
}

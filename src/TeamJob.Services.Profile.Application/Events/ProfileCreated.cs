using System;
using Convey.CQRS.Events;

namespace TeamJob.Services.Profile.Application.Events
{
    public class ProfileCreated : IEvent
    {
        public Guid Id     { get; }
        public string Role { get; }

        public ProfileCreated(Guid id, string role)
        {
            Id   = id;
            Role = role;

        }
    }
}

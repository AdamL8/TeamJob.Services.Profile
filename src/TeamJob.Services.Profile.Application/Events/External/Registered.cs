using System;
using System.Collections.Generic;
using System.Text;
using Convey.CQRS.Events;
using Convey.MessageBrokers;

namespace TeamJob.Services.Profile.Application.Events.External
{
    [Message(exchange: "identity", external: true)]
    public class Registered : IEvent
    {
        public string Id      { get; }
        public string Email { get; }
        public string Role  { get; }

        public Registered(string id, string email, string role)
        {
            Id    = id;
            Email = email;
            Role  = role;
        }
    }
}

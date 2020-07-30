using System;
using System.Collections.Generic;
using System.Text;
using Convey.CQRS.Events;

namespace TeamJob.Services.Profile.Application.Events.Rejected
{
    public class DeleteProfileRejected : IRejectedEvent
    {
        public Guid Id       { get; }
        public string Reason { get; }
        public string Code   { get; }

        public DeleteProfileRejected(Guid id, string reason, string code)
        {
            Id     = id;
            Reason = reason;
            Code   = code;
        }
    }
}

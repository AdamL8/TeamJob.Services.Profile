using System;
using System.Collections.Generic;
using System.Text;
using Convey.CQRS.Events;
using TeamJob.Services.Profile.Core.Entities;

namespace TeamJob.Services.Profile.Application.Services
{
    public interface IEventMapper
    {
        IEvent Map(IDomainEvent @event);
        IEnumerable<IEvent> MapAll(IEnumerable<IDomainEvent> events);
    }
}

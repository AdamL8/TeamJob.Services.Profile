using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using Convey.CQRS.Events;
using TeamJob.Services.Profile.Application.Services;
using TeamJob.Services.Profile.Core.Entities;
using TeamJob.Services.Profile.Core.Events;

namespace TeamJob.Services.Profile.Infrastructure.Services
{
    public class EventMapper : IEventMapper
    {
        public IEnumerable<IEvent> MapAll(IEnumerable<IDomainEvent> events)
            => events.Select(Map);

        public IEvent Map(IDomainEvent @event)
        {
            switch (@event)
            {
                case UserProfileRegistrationCompleted e:
                    return new Application.Events.ProfileCreated(e.UserProfile.Id, e.UserProfile.Role.ToString().ToLowerInvariant());
                case UserProfileStateChanged e:
                    return new Application.Events.UserProfileStateChanged(e.UserProfile.Id,
                                                                          e.UserProfile.State.ToString().ToLowerInvariant(),
                                                                          e.PreviousState.ToString().ToLowerInvariant());
            }

            return null;
        }
    }
}
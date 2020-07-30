using System;
using System.Collections.Generic;
using System.Text;
using Convey.CQRS.Events;

namespace TeamJob.Services.Profile.Application.Events
{
    public class UserProfileStateChanged : IEvent
    {
        public Guid Id              { get; }
        public string CurrentState  { get; }
        public string PreviousState { get; }

        public UserProfileStateChanged(Guid id, string currentState, string previousState)
        {
            Id            = id;
            CurrentState  = currentState;
            PreviousState = previousState;
        }
    }
}

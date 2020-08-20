using System;
using System.Collections.Generic;
using System.Text;
using TeamJob.Services.Profile.Core.Entities;

namespace TeamJob.Services.Profile.Core.Events
{
    public class UserProfileStateChanged : IDomainEvent
    {
        public UserProfile UserProfile { get; }
        public State PreviousState { get; }

        public UserProfileStateChanged(UserProfile userProfile, State previousState)
        {
            UserProfile   = userProfile;
            PreviousState = previousState;
        }
    }
}

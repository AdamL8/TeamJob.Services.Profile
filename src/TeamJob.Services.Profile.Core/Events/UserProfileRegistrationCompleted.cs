using System;
using System.Collections.Generic;
using System.Text;
using TeamJob.Services.Profile.Core.Entities;

namespace TeamJob.Services.Profile.Core.Events
{
    public class UserProfileRegistrationCompleted : IDomainEvent
    {
        public UserProfile UserProfile { get; }

        public UserProfileRegistrationCompleted(UserProfile userProfile)
        {
            UserProfile = userProfile;
        }
    }
}

using System;

namespace TeamJob.Services.Profile.Application.Exceptions
{
    public class UserProfileAlreadyCompletedException : AppException
    {
        public override string Code { get; } = "service.profile.exception.user_profile_already_completed";
        public Guid UserProfile { get; }
        
        public UserProfileAlreadyCompletedException(Guid userProfile) 
            : base($"User profile with id: {userProfile} has already a completed registration.")
        {
            UserProfile = userProfile;
        }
    }
}
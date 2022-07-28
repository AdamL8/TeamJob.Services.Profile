using System;

namespace TeamJob.Services.Profile.Application.Exceptions
{
    public class UserProfileAlreadyCompletedException : AppException
    {
        public override string Code { get; } = "service.profile.exception.user_profile_already_completed";
        public string UserProfile { get; }
        
        public UserProfileAlreadyCompletedException(string userProfile) 
            : base($"User profile with id: {userProfile} has already a completed registration.")
        {
            UserProfile = userProfile;
        }
    }
}
using System;

namespace TeamJob.Services.Profile.Application.Exceptions
{
    public class UserProfileAlreadyRegisteredException : AppException
    {
        public override string Code { get; } = "service.profile.exception.user_profile_already_registered";
        public Guid UserProfile { get; }
        
        public UserProfileAlreadyRegisteredException(Guid userProfile) 
            : base($"User profile with id: {userProfile} has already been registered.")
        {
            UserProfile = userProfile;
        }
    }
}
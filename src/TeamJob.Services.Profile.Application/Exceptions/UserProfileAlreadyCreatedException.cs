using System;

namespace TeamJob.Services.Profile.Application.Exceptions
{
    public class UserProfileAlreadyCreatedException: AppException
    {
        public override string Code { get; } = "service.profile.exception.user_profile_already_created";
        public string Id { get; }

        public UserProfileAlreadyCreatedException(string id)
            : base($"User profile with id: {id} was already created.")
        {
            Id = id;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace TeamJob.Services.Profile.Application.Exceptions
{
    public class UserProfileNotFoundException : AppException
    {
        public override string Code { get; } = "service.profile.exception.user_profile_not_found";
        public string Id { get; }

        public UserProfileNotFoundException(string id)
            : base($"User profile with id: {id} was not found.")
        {
            Id = id;
        }
    }
}

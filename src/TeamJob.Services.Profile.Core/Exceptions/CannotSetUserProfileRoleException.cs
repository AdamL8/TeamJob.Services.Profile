using System;
using System.Collections.Generic;
using System.Text;
using TeamJob.Services.Profile.Core.Entities;

namespace TeamJob.Services.Profile.Core.Exceptions
{
    public class CannotSetUserProfileRoleException : DomainException
    {
        public override string Code { get; } = "service.profile.exception.cannot_set_user_profile_role";

        public string Id     { get; }
        public string Role { get; }

        public CannotSetUserProfileRoleException(string id, string role)
            : base($"Cannot set user profile: {id} role to: {role}.")
        {
            Id   = id;
            Role = role;
        }
    }
}

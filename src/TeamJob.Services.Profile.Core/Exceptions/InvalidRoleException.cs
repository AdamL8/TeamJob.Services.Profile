using System;
using System.Collections.Generic;
using System.Text;

namespace TeamJob.Services.Profile.Core.Exceptions
{
    public class InvalidRoleException : DomainException
    {
        public override string Code { get; } = "service.profile.exception.invalid_role";

        public InvalidRoleException(string userId, string role, string requiredRole)
            : base($"User profile will not be created for the user with id: {userId} " +
                   $"due to the invalid role: {role} (required: {requiredRole}).")
        {
        }
    }
}

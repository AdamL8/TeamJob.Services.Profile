using System;
using System.Collections.Generic;
using System.Text;

namespace TeamJob.Services.Profile.Application.Exceptions
{
    public class InvalidRoleException : AppException
    {
        public override string Code { get; } = "service.profile.exception.invalid_role";

        public InvalidRoleException(Guid userId, string role, string requiredRole)
            : base($"User profile will not be created for the user with id: {userId} " +
                   $"due to the invalid role: {role} (required: {requiredRole}).")
        {
        }
    }
}

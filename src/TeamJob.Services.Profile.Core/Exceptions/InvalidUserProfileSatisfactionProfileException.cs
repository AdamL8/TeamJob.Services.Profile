using System;
using System.Collections.Generic;
using System.Text;

namespace TeamJob.Services.Profile.Core.Exceptions
{
    public class InvalidUserProfileSatisfactionProfileException : DomainException
    {
        public override string Code { get; } = "service.profile.exception.invalid_user_profile_satisfaction_profile";
        public Guid Id { get; }

        public InvalidUserProfileSatisfactionProfileException(Guid id)
            : base($"User profile with id: {id} has invalid satisfaction profile.")
        {
            Id = id;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using TeamJob.Services.Profile.Core.Entities;

namespace TeamJob.Services.Profile.Core.Exceptions
{
    public class IncompleteUserProfileException : DomainException
    {
        public override string Code { get; } = "service.profile.exception.incomplete_user_profile";

        public Guid Id { get; }

        public IncompleteUserProfileException(Guid id)
            : base($"User profile: {id} is incomplete.")
        {
            Id   = id;
        }
    }
}

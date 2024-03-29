﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TeamJob.Services.Profile.Core.Exceptions
{
    public class InvalidUserProfilePersonalInformationException : DomainException
    {
        public override string Code { get; } = "service.profile.exception.invalid_user_profile_personal_information";
        public Guid Id { get; }

        public InvalidUserProfilePersonalInformationException(Guid id)
            : base($"User profile with id: {id} has invalid personal information.")
        {
            Id = id;
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Text;
using TeamJob.Services.Profile.Core.Entities;

namespace TeamJob.Services.Profile.Core.Exceptions
{
    public class IncompleteUserProfileException : DomainException
    {
        public override string Code { get; } = "service.profile.exception.incomplete_user_profile";

        public string Id { get; }

        public IncompleteUserProfileException(string id)
            : base($"User profile: {id} is incomplete.")
        {
            Id   = id;
        }
    }
}

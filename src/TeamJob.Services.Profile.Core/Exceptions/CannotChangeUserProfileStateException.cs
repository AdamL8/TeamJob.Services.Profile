using System;
using System.Collections.Generic;
using System.Text;
using TeamJob.Services.Profile.Core.Entities;

namespace TeamJob.Services.Profile.Core.Exceptions
{
    public class CannotChangeUserProfileStateException : DomainException
    {
        public override string Code { get; } = "service.profile.exception.cannot_change_user_profile_state";

        public Guid Id     { get; }
        public State State { get; }

        public CannotChangeUserProfileStateException(Guid id, State state)
            : base($"Cannot change user profile: {id} state to: {state}.")
        {
            Id    = id;
            State = state;
        }
    }
}

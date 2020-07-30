using System;
using System.Collections.Generic;
using System.Text;

namespace TeamJob.Services.Profile.Core.Exceptions
{
    public class InvalidAggregateIdException : DomainException
    {
        public override string Code { get; } = "service.profile.exception.invalid_aggregate_id";

        public InvalidAggregateIdException()
            : base($"Invalid aggregate id.")
        {
        }
    }
}

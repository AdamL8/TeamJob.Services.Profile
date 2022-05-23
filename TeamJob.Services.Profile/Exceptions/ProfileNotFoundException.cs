using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamJob.Services.Profile.Exceptions
{
    public class ProfileNotFoundException : TeamJobException
    {
        public override string Code { get; } = "profile_not_found";

        public ProfileNotFoundException(string InProfileId)
            : base($"Profile with ID [{InProfileId}] was not found.")
        {
        }
    }
}

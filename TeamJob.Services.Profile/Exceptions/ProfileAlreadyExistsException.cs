using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamJob.Services.Profile.Exceptions
{
    public class ProfileAlreadyExistsException : TeamJobException
    {
        public override string Code { get; } = "profile_already_exists";

        public ProfileAlreadyExistsException(string InProfileId)
            : base($"Profile with ID [{InProfileId}] already exists.")
        {
        }
    }
}

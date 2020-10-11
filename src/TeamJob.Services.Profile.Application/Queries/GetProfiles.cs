using System;
using System.Collections.Generic;
using System.Text;
using Convey.CQRS.Queries;
using Newtonsoft.Json;
using TeamJob.Services.Profile.Application.DTO;

namespace TeamJob.Services.Profile.Application.Queries
{
    public class GetProfiles : IQuery<IEnumerable<UserProfileDetailsDto>>
    {
        public string Role { get; }

        [JsonConstructor]
        public GetProfiles(string role)
        {
            Role = role;
        }
    }
}

using Convey.CQRS.Queries;
using Newtonsoft.Json;
using System.Collections.Generic;
using TeamJob.Services.Profile.DTO;

namespace TeamJob.Services.Profile.Queries
{
    public class GetProfiles : PagedQueryBase, IQuery<IEnumerable<ProfileDto>>
    {
        public string Role { get; }

        [JsonConstructor]
        public GetProfiles(string role)
        {
            Role = role;
        }
    }
}

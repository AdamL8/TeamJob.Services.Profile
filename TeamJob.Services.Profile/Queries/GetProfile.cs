using Convey.CQRS.Queries;
using Newtonsoft.Json;
using System;
using TeamJob.Services.Profile.DTO;

namespace TeamJob.Services.Profile.Queries
{
    public class GetProfile : IQuery<ProfileDto>
    {
        public Guid ProfileId { get; }

        [JsonConstructor]
        public GetProfile(Guid profileId)
        {
            ProfileId = profileId;
        }
    }
}

using Convey.CQRS.Queries;
using Newtonsoft.Json;
using System;
using TeamJob.Services.Profile.DTO;

namespace TeamJob.Services.Profile.Queries
{
    public class GetProfile : IQuery<ProfileDto>
    {
        public string Id { get; }

        [JsonConstructor]
        public GetProfile(string id)
        {
            Id = id;
        }
    }
}

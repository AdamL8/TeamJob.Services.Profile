using Convey.CQRS.Queries;
using Newtonsoft.Json;
using System;
using TeamJob.Services.Profile.DTO;

namespace TeamJob.Services.Profile.Queries
{
    public class GetProfile : IQuery<ProfileDto>
    {
        public Guid Id { get; }

        [JsonConstructor]
        public GetProfile(Guid id)
        {
            Id = id;
        }
    }
}

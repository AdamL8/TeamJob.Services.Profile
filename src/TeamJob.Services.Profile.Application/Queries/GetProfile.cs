using System;
using System.Collections.Generic;
using System.Text;
using Convey.CQRS.Queries;
using Newtonsoft.Json;
using TeamJob.Services.Profile.Application.DTO;

namespace TeamJob.Services.Profile.Application.Queries
{
    public class GetProfile : IQuery<UserProfileDetailsDto>
    {
        public string Id { get; }

        [JsonConstructor]
        public GetProfile(string id)
        {
            Id = id;
        }
    }
}

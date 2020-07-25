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
        public Guid Id { get; }

        [JsonConstructor]
        public GetProfile(Guid id)
        {
            Id = id;
        }
    }
}

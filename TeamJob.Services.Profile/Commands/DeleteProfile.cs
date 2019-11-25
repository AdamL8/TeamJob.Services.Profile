using Convey.CQRS.Commands;
using Newtonsoft.Json;
using System;

namespace TeamJob.Services.Profile.Commands
{
    public class DeleteProfile : ICommand
    {
        public Guid ProfileId { get; }

        [JsonConstructor]
        public DeleteProfile(Guid profileId)
        {
            ProfileId = profileId;
        }
    }
}

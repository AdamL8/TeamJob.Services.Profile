using Convey.CQRS.Commands;
using Newtonsoft.Json;
using System;

namespace TeamJob.Services.Profile.Commands
{
    public class DeleteProfile : ICommand
    {
        public string Id { get; }

        [JsonConstructor]
        public DeleteProfile(string id)
        {
            Id = id;
        }
    }
}

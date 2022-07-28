using System;
using System.Collections.Generic;
using System.Text;
using Convey.CQRS.Commands;
using Newtonsoft.Json;

namespace TeamJob.Services.Profile.Application.Commands
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

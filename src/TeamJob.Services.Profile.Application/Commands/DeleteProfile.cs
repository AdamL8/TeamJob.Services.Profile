using System;
using System.Collections.Generic;
using System.Text;
using Convey.CQRS.Commands;
using Newtonsoft.Json;

namespace TeamJob.Services.Profile.Application.Commands
{
    public class DeleteProfile : ICommand
    {
        public Guid Id { get; }

        [JsonConstructor]
        public DeleteProfile(Guid id)
        {
            Id = id;
        }
    }
}

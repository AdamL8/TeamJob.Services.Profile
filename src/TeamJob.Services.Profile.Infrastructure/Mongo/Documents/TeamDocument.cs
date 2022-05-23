using System;
using System.Collections.Generic;
using System.Text;
using TeamJob.Services.Profile.Core.Entities;

namespace TeamJob.Services.Profile.Infrastructure.Mongo.Documents
{
    public class TeamDocument
    {
        public string Id                 { get; set; }
        public string Name             { get; set; }
        public TeamMemberStatus Status { get; set; }
    }
}

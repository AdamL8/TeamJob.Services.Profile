using System;
using System.Collections.Generic;
using System.Text;
using Convey.Types;
using TeamJob.Services.Profile.Core.Entities;

namespace TeamJob.Services.Profile.Infrastructure.Mongo.Documents
{
    public class UserProfileDocument : IIdentifiable<Guid>
    {
        public Guid Id                                         { get; set; }
        public string Email                                    { get; set; }
        public PersonalInformationDocument PersonalInformation { get; set; }
        public SatisfactionProfileDocument SatisfactionProfile { get; set; }
        public Role Role                                       { get; set; }
        public State State                                     { get; set; }
        public long CreatedAt                                  { get; set; }
        public IEnumerable<TeamDocument> Teams                 { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace TeamJob.Services.Profile.Infrastructure.Mongo.Documents
{
    public class PersonalInformationDocument
    {
        public string FirstName                              { get; set; }
        public string LastName                               { get; set; }
        public ContactInformationDocument ContactInformation { get; set; }
        public AddressDocument Address                       { get; set; }
        public long DateOfBirth                              { get; set; }
    }
}

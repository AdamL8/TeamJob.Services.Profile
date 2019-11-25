using Newtonsoft.Json;
using System;

namespace TeamJob.Services.Profile.Domain
{
    public class PersonalInformation
    {
        public string FirstName                      { get; private set; }
        public string LastName                       { get; private set; }
        public ContactInformation ContactInformation { get; private set; }
        public Address Address                       { get; private set; }
        public DateTime DateOfBirth                  { get; private set; }

        [JsonConstructor]
        public PersonalInformation(string             firstName,
                                   string             lastName,
                                   ContactInformation contactInformation,
                                   Address            address,
                                   DateTime           dateOfBirth)
        {
            FirstName          = firstName;
            LastName           = lastName;
            ContactInformation = contactInformation;
            Address            = address;
            DateOfBirth        = dateOfBirth;
        }
    }
}

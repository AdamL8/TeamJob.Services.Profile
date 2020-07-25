namespace TeamJob.Services.Profile.Core.Entities
{
    public class PersonalInformation
    {
        public string FirstName                      { get; private set; }
        public string LastName                       { get; private set; }
        public ContactInformation ContactInformation { get; private set; }
        public Address Address                       { get; private set; }
        public long DateOfBirth                      { get; private set; }

        public PersonalInformation(string             firstName,
                                   string             lastName,
                                   ContactInformation contactInformation,
                                   Address            address,
                                   long               dateOfBirth)
        {
            FirstName          = firstName;
            LastName           = lastName;
            ContactInformation = contactInformation;
            Address            = address;
            DateOfBirth        = dateOfBirth;
        }
    }
}
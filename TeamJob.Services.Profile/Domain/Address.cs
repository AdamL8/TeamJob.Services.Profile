using Newtonsoft.Json;

namespace TeamJob.Services.Profile.Domain
{
    public class Address
    {
        public string Country     { get; private set; }
        public string Province    { get; private set; }
        public string City        { get; private set; }
        public string AddressLine { get; private set; }
        public string PostalCode  { get; private set; }

        [JsonConstructor]
        public Address(string country,
                       string province,
                       string city,
                       string addressLine,
                       string postalCode)
        {
            Country     = country;
            Province    = province;
            City        = city;
            AddressLine = addressLine;
            PostalCode  = postalCode;
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamJob.Services.Profile.Domain
{
    public class ContactInformation
    {
        public string CellPhoneNumber { get; private set; }
        public string HomePhoneNumber { get; private set; }

        [JsonConstructor]
        public ContactInformation(string cellPhoneNumber, string homePhoneNumber)
        {
            CellPhoneNumber = cellPhoneNumber;
            HomePhoneNumber = homePhoneNumber;
        }
    }
}

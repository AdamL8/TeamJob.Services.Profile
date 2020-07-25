using System;
using System.Collections.Generic;
using System.Text;

namespace TeamJob.Services.Profile.Infrastructure.Mongo.Documents
{
    public class AddressDocument
    {
        public string Country     { get; set; }
        public string Province    { get; set; }
        public string City        { get; set; }
        public string AddressLine { get; set; }
        public string PostalCode  { get; set; }
    }
}

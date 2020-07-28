namespace TeamJob.Services.Profile.Core.Entities
{
    public class ContactInformation
    {
        public string CellPhoneNumber { get; private set; }
        public string HomePhoneNumber { get; private set; }

        public ContactInformation(string cellPhoneNumber, string homePhoneNumber)
        {
            CellPhoneNumber = cellPhoneNumber;
            HomePhoneNumber = homePhoneNumber;
        }
    }
}
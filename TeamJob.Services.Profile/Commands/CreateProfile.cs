using Convey.CQRS.Commands;
using Newtonsoft.Json;
using System;
using TeamJob.Services.Profile.Domain;

namespace TeamJob.Services.Profile.Commands
{
    public class CreateProfile : ICommand
    {
        public Guid ProfileId                          { get; }
        public PersonalInformation PersonalInformation { get; }
        public SatisfactionProfile SatisfactionProfile { get; }
        public string Role                             { get; }

        [JsonConstructor]
        public CreateProfile(Guid                profileId,
                             PersonalInformation personalInformation,
                             SatisfactionProfile satisfactionProfile,
                             string              role)
        {
            ProfileId           = profileId;
            PersonalInformation = personalInformation;
            SatisfactionProfile = satisfactionProfile;
            Role                = role;
        }
    }
}

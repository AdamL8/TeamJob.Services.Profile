using Convey.CQRS.Commands;
using Newtonsoft.Json;
using System;
using TeamJob.Services.Profile.Domain;

namespace TeamJob.Services.Profile.Commands
{
    public class CreateProfile : ICommand
    {
        public string Id                                 { get; }
        public PersonalInformation PersonalInformation { get; }
        public SatisfactionProfile SatisfactionProfile { get; }
        public string Role                             { get; }

        [JsonConstructor]
        public CreateProfile(string                id,
                             PersonalInformation personalInformation,
                             SatisfactionProfile satisfactionProfile,
                             string              role)
        {
            Id                  = id;
            PersonalInformation = personalInformation;
            SatisfactionProfile = satisfactionProfile;
            Role                = role;
        }
    }
}

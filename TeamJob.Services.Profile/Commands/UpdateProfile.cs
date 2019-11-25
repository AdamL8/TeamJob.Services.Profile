using Convey.CQRS.Commands;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using TeamJob.Services.Profile.Domain;

namespace TeamJob.Services.Profile.Commands
{
    public class UpdateProfile : ICommand
    {
        public Guid ProfileId                          { get; }
        public PersonalInformation PersonalInformation { get; }
        public SatisfactionProfile SatisfactionProfile { get; }
        public List<Team> Teams                        { get; }

        [JsonConstructor]
        public UpdateProfile(Guid                profileId,
                             PersonalInformation personalInformation,
                             SatisfactionProfile satisfactionProfile,
                             List<Team>          teams)
        {
            ProfileId           = profileId;
            PersonalInformation = personalInformation;
            SatisfactionProfile = satisfactionProfile;
            Teams               = teams;
        }
    }
}

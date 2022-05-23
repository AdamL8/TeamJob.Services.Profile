using Convey.CQRS.Commands;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using TeamJob.Services.Profile.Domain;

namespace TeamJob.Services.Profile.Commands
{
    public class UpdateProfile : ICommand
    {
        public string Id                                 { get; }
        public PersonalInformation PersonalInformation { get; }
        public SatisfactionProfile SatisfactionProfile { get; }
        public List<Team> Teams                        { get; }

        [JsonConstructor]
        public UpdateProfile(string                id,
                             PersonalInformation personalInformation,
                             SatisfactionProfile satisfactionProfile,
                             List<Team>          teams)
        {
            Id                  = id;
            PersonalInformation = personalInformation;
            SatisfactionProfile = satisfactionProfile;
            Teams               = teams;
        }
    }
}

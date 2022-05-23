using System;
using System.Collections.Generic;
using System.Text;
using Convey.CQRS.Commands;
using Newtonsoft.Json;
using TeamJob.Services.Profile.Core.Entities;

namespace TeamJob.Services.Profile.Application.Commands
{
    public class CompleteUserProfileRegistration : ICommand
    {
        public string Id                                 { get; }
        public PersonalInformation PersonalInformation { get; }
        public SatisfactionProfile SatisfactionProfile { get; }
        public string Role                             { get; }

        [JsonConstructor]
        public CompleteUserProfileRegistration(string                id,
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

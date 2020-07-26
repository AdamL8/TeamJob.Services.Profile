using System;
using System.Collections.Generic;
using System.Text;
using Convey.CQRS.Commands;
using Newtonsoft.Json;
using TeamJob.Services.Profile.Core.Entities;

namespace TeamJob.Services.Profile.Application.Commands
{
    public class UpdateProfile : ICommand
    {
        public Guid Id                                 { get; }
        public PersonalInformation PersonalInformation { get; }
        public SatisfactionProfile SatisfactionProfile { get; }
        public IEnumerable<Team> Teams                 { get; }

        [JsonConstructor]
        public UpdateProfile(Guid                id,
                             PersonalInformation personalInformation,
                             SatisfactionProfile satisfactionProfile,
                             IEnumerable<Team>   teams)
        {
            Id                  = id;
            PersonalInformation = personalInformation;
            SatisfactionProfile = satisfactionProfile;
            Teams               = teams;
        }
    }
}

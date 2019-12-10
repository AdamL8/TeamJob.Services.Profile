using Convey.Types;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace TeamJob.Services.Profile.Domain
{
    public class UserProfile : IIdentifiable<Guid>
    {
        public Guid Id                                 { get; private set; }
        public PersonalInformation PersonalInformation { get; private set; }
        public SatisfactionProfile SatisfactionProfile { get; private set; }
        public Role Role                               { get; private set; }
        public List<Team> Teams                        { get; private set; }
        public long CreatedAt                          { get; private set; }
        public long UpdatedAt                          { get; private set; }



        [JsonConstructor]
        public UserProfile(Guid                id,
                           PersonalInformation personalInformation,
                           SatisfactionProfile satisfactionProfile,
                           Role                role)
            : this(id, personalInformation, satisfactionProfile, role, new List<Team>(), DateTimeOffset.UtcNow.ToUnixTimeSeconds())
        {            
        }

        public UserProfile(Guid                id,
                           PersonalInformation personalInformation,
                           SatisfactionProfile satisfactionProfile,
                           Role                role,
                           List<Team>          teams,
                           long                createdAt)
        {
            Id                  = id;
            PersonalInformation = personalInformation;
            SatisfactionProfile = satisfactionProfile;
            Role                = role;
            Teams               = teams;
            CreatedAt           = createdAt;
            UpdatedAt           = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        }
    }
}

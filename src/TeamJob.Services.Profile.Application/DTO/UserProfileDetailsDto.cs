using System;
using System.Collections.Generic;
using TeamJob.Services.Profile.Core.Entities;

namespace TeamJob.Services.Profile.Application.DTO
{
    public class UserProfileDetailsDto : UserProfileDto
    {
        public string Email                            { get; set; }
        public PersonalInformation PersonalInformation { get; set; }
        public SatisfactionProfile SatisfactionProfile { get; set; }
        public string Role                             { get; set; }
        public IEnumerable<Team> Teams                 { get; set; }
    }
}
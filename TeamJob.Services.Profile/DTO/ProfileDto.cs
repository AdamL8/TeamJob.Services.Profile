﻿using System;
using System.Collections.Generic;
using TeamJob.Services.Profile.Domain;

namespace TeamJob.Services.Profile.DTO
{
    public class ProfileDto
    {
        public Guid Id                                 { get; set; }
        public PersonalInformation PersonalInformation { get; set; }
        public SatisfactionProfile SatisfactionProfile { get; set; }
        public Role Role                               { get; set; }
        public List<Team> Teams                        { get; set; }
        public DateTime CreatedAt                      { get; set; }
        public DateTime UpdatedAt                      { get; set; }
    }
}
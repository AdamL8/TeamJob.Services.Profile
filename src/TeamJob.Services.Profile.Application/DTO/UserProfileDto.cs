using System;

namespace TeamJob.Services.Profile.Application.DTO
{
    public class UserProfileDto
    {
        public Guid Id        { get; set; }
        public string State   { get; set; }
        public long CreatedAt { get; set; }
    }
}
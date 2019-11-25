using TeamJob.Services.Profile.Domain;
using TeamJob.Services.Profile.DTO;

namespace TeamJob.Services.Profile
{
    public static class Extensions
    {
        public static ProfileDto AsDto(this UserProfile InProfile)
            => new ProfileDto
            {
                Id                  = InProfile.Id,
                PersonalInformation = InProfile.PersonalInformation,
                Role                = InProfile.Role,
                CreatedAt           = InProfile.CreatedAt,
                SatisfactionProfile = InProfile.SatisfactionProfile,
                Teams               = InProfile.Teams,
                UpdatedAt           = InProfile.UpdatedAt
            };
    }
}

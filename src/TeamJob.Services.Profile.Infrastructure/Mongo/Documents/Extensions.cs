using TeamJob.Services.Profile.Application.DTO;
using TeamJob.Services.Profile.Core.Entities;

namespace TeamJob.Services.Profile.Infrastructure.Mongo.Documents
{
    public static class Extensions
    {
        public static UserProfile AsEntity(this UserProfileDocument document)
            => new UserProfile(document.Id,
                               document.Email,
                               document.PersonalInformation,
                               document.SatisfactionProfile,
                               document.Role,
                               document.State,
                               document.Teams,
                               document.CreatedAt);

        public static UserProfileDocument AsDocument(this UserProfile entity)
            => new UserProfileDocument
            {
                Id                  = entity.Id,
                Email               = entity.Email,
                PersonalInformation = entity.PersonalInformation,
                SatisfactionProfile = entity.SatisfactionProfile,
                Role                = entity.Role,
                State               = entity.State,
                Teams               = entity.Teams,
                CreatedAt           = entity.CreatedAt
            };

        public static UserProfileDto AsDto(this UserProfileDocument document)
            => new UserProfileDto
            {
                Id        = document.Id,
                State     = document.State.ToString().ToLowerInvariant(),
                CreatedAt = document.CreatedAt,
            };

        public static UserProfileDetailsDto AsDetailsDto(this UserProfileDocument document)
            => new UserProfileDetailsDto
            {
                Id                  = document.Id,
                Email               = document.Email,
                PersonalInformation = document.PersonalInformation,
                SatisfactionProfile = document.SatisfactionProfile,
                Role                = document.Role.ToString().ToLowerInvariant(),
                State               = document.State.ToString().ToLowerInvariant(),
                Teams               = document.Teams,
                CreatedAt           = document.CreatedAt
            };
    }
}
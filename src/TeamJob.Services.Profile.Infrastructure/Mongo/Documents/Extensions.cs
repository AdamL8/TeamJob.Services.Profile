using System.Linq;
using TeamJob.Services.Profile.Application.DTO;
using TeamJob.Services.Profile.Core.Entities;

namespace TeamJob.Services.Profile.Infrastructure.Mongo.Documents
{
    public static class Extensions
    {
        public static UserProfile AsEntity(this UserProfileDocument document)
            => new UserProfile(document.Id,
                               document.Email,
                               document.PersonalInformation?.AsEntity(),
                               document.SatisfactionProfile?.AsEntity(),
                               document.Role,
                               document.State,
                               document.Teams.Select(x => x.AsEntity()),
                               document.CreatedAt);

        public static UserProfileDocument AsDocument(this UserProfile entity)
            => new UserProfileDocument
            {
                Id                  = entity.Id,
                Email               = entity.Email,
                PersonalInformation = entity.PersonalInformation?.AsDocument(),
                SatisfactionProfile = entity.SatisfactionProfile?.AsDocument(),
                Role                = entity.Role,
                State               = entity.State,
                Teams               = entity.Teams.Select(x => x.AsDocument()),
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
                PersonalInformation = document.PersonalInformation?.AsEntity(),
                SatisfactionProfile = document.SatisfactionProfile?.AsEntity(),
                Role                = document.Role.ToString().ToLowerInvariant(),
                State               = document.State.ToString().ToLowerInvariant(),
                Teams               = document.Teams.Select(x => x.AsEntity()),
                CreatedAt           = document.CreatedAt
            };

        public static Address AsEntity(this AddressDocument document)
            => new Address(document.Country,
                           document.Province,
                           document.City,
                           document.AddressLine,
                           document.PostalCode);

        public static AddressDocument AsDocument(this Address entity)
            => new AddressDocument
            {
                Country     = entity.Country,
                City        = entity.City,
                AddressLine = entity.AddressLine,
                PostalCode  = entity.PostalCode,
                Province    = entity.Province
            };

        public static ContactInformation AsEntity(this ContactInformationDocument document)
            => new ContactInformation(document.CellPhoneNumber,
                                      document.HomePhoneNumber);

        public static ContactInformationDocument AsDocument(this ContactInformation entity)
            => new ContactInformationDocument
            {
                CellPhoneNumber = entity.CellPhoneNumber,
                HomePhoneNumber = entity.HomePhoneNumber
            };

        public static PersonalInformation AsEntity(this PersonalInformationDocument document)
            => new PersonalInformation(document.FirstName,
                                       document.LastName,
                                       document.ContactInformation?.AsEntity(),
                                       document.Address?.AsEntity(),
                                       document.DateOfBirth);

        public static PersonalInformationDocument AsDocument(this PersonalInformation entity)
            => new PersonalInformationDocument
            {
                FirstName          = entity.FirstName,
                LastName           = entity.LastName,
                ContactInformation = entity.ContactInformation?.AsDocument(),
                Address            = entity.Address?.AsDocument(),
                DateOfBirth        = entity.DateOfBirth
            };

        public static SatisfactionProfile AsEntity(this SatisfactionProfileDocument document)
            => new SatisfactionProfile(document.MinHours,
                                       document.MaxHours,
                                       document.AvgHours,
                                       document.Skills,
                                       document.Limits,
                                       document.IsAspiringLeader,
                                       document.IsAspiringTrainer);

        public static SatisfactionProfileDocument AsDocument(this SatisfactionProfile entity)
            => new SatisfactionProfileDocument
            {
                MinHours          = entity.MinHours,
                MaxHours          = entity.MaxHours,
                AvgHours          = entity.AvgHours,
                Skills            = entity.Skills,
                Limits            = entity.Limits,
                IsAspiringLeader  = entity.IsAspiringLeader,
                IsAspiringTrainer = entity.IsAspiringTrainer,
            };

        public static Team AsEntity(this TeamDocument document)
            => new Team(document.Id,
                        document.Name,
                        document.Status);

        public static TeamDocument AsDocument(this Team entity)
            => new TeamDocument
            {
                Id     = entity.Id,
                Name   = entity.Name,
                Status = entity.Status
            };
    }
}
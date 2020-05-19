using FluentValidation;
using System;
using TeamJob.Services.Profile.Domain;

namespace TeamJob.Services.Profile.Queries.Validations
{
    public class GetProfilesValidator : AbstractValidator<GetProfiles>
    {
        public GetProfilesValidator()
        {
            RuleFor(x => x.Role)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage("Role cannot be empty")
                .NotEmpty().WithMessage("Role cannot be empty")
                .Must(IsValidRole).WithMessage("Invalid Role");
        }

        private bool IsValidRole(string InRole)
        {
            return string.IsNullOrEmpty(InRole)
                        ? true
                        : Enum.IsDefined(typeof(Role), InRole);
        }
    }
}

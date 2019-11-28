using FluentValidation;
using System;
using TeamJob.Services.Profile.Domain;

namespace TeamJob.Services.Profile.Commands.Validations
{
    public class CreateProfileValidator : AbstractValidator<CreateProfile>
    {
        public CreateProfileValidator()
        {
            RuleFor(x => x.Id)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage("Profile ID cannot be empty")
                .NotEmpty().WithMessage("Profile ID cannot be empty");

            RuleFor(x => x.Role)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage("Role cannot be empty")
                .NotEmpty().WithMessage("Role cannot be empty")
                .Must(IsValidRole).WithMessage("Invalid Role");

            // TODO: Finish this
        }

        private bool IsValidRole(string InRole)
        {
            return Enum.IsDefined(typeof(Role), InRole);
        }
    }
}

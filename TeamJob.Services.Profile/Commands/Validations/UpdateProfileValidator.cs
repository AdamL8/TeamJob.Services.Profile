using FluentValidation;
using System;
using TeamJob.Services.Profile.Domain;

namespace TeamJob.Services.Profile.Commands.Validations
{
    public class UpdateProfileValidator : AbstractValidator<UpdateProfile>
    {
        public UpdateProfileValidator()
        {
            RuleFor(x => x.Id)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage("Profile ID cannot be empty")
                .NotEmpty().WithMessage("Profile ID cannot be empty");

            // TODO: Finish this
        }
    }
}

using FluentValidation;

namespace TeamJob.Services.Profile.Commands.Validations
{
    public class DeleteProfileValidator : AbstractValidator<DeleteProfile>
    {
        public DeleteProfileValidator()
        {
            RuleFor(x => x.Id)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage("Profile ID cannot be empty")
                .NotEmpty().WithMessage("Profile ID cannot be empty");
        }
    }
}

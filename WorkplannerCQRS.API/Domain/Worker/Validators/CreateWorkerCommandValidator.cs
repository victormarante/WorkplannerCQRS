using FluentValidation;
using WorkplannerCQRS.API.Domain.Worker.Commands;

namespace WorkplannerCQRS.API.Domain.Worker.Validators
{
    public class CreateWorkerCommandValidator : AbstractValidator<CreateWorkerCommand>
    {
        public CreateWorkerCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name cannot be empty");
            
            // TODO: Add proper validation for phone numbers
            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .WithMessage("Must enter a phone number");

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();
        }
    }
}
using FluentValidation;
using WorkplannerCQRS.API.Domain.WorkOrder.Commands;

namespace WorkplannerCQRS.API.Domain.WorkOrder.Validators
{
    public class CreateWorkOrderCommandValidator : AbstractValidator<CreateWorkOrderCommand>
    {
        public CreateWorkOrderCommandValidator()
        {
            RuleFor(x => x.ObjectNumber)
                .NotEmpty()
                .WithMessage("Object number cannot be empty");
            
            RuleFor(x => x.Address)
                .NotEmpty()
                .Length(1, 50).WithMessage("Address must be between 1 - 50 characters");
        }
    }
}
using bART_Solutions_task.Core.Dto;
using FluentValidation;

namespace bART_Solutions_task.Core.FluentValidation;

public class CreateIncidentDtoValidator : AbstractValidator<CreateIncidentDto>
{
    public CreateIncidentDtoValidator()
    {
        RuleFor(x => x.Description).NotNull().NotEmpty().WithMessage("Description is required");
        RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.FirstName).NotNull().NotEmpty().WithMessage("First Name is required");
        RuleFor(x => x.LastName).NotNull().NotEmpty().WithMessage("Last Name is required");
        RuleFor(x => x.Email).EmailAddress().WithMessage("Email is required");
    }
}
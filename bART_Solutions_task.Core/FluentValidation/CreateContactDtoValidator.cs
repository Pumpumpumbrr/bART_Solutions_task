using bART_Solutions_task.Core.Dto;
using FluentValidation;

namespace bART_Solutions_task.Core.FluentValidation;

public class CreateContactDtoValidator : AbstractValidator<CreateContactDto>
{
    public CreateContactDtoValidator()
    {
        RuleFor(x => x.FirstName).NotNull().NotEmpty().WithMessage("First Name is required");
        RuleFor(x => x.LastName).NotNull().NotEmpty().WithMessage("Last Name is required");
        RuleFor(x => x.Email).EmailAddress().WithMessage("Email is required");
    }
}
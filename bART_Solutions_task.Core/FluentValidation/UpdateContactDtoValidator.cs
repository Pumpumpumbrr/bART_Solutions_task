using bART_Solutions_task.Core.Dto;
using bART_Solutions_task.Core.Services.Interfaces;
using FluentValidation;

namespace bART_Solutions_task.Core.FluentValidation;

public class UpdateContactDtoValidator : AbstractValidator<UpdateContactDto>
{
    private readonly IContactService _contactService;
    public UpdateContactDtoValidator(IContactService contactService)
    {
        _contactService = contactService;
        
        RuleFor(x => x.FirstName).NotNull().NotEmpty().WithMessage("First Name is required");
        RuleFor(x => x.LastName).NotNull().NotEmpty().WithMessage("Last Name is required");
        RuleFor(x => x.Email).EmailAddress().WithMessage("Email is required");

        RuleFor(x => x.Email).MustAsync(async (x, cancellationToken) =>
        {
            bool result = await _contactService.IsInSystem(x);
            return result;
        }).WithMessage("Contact not found").WithErrorCode("404");
    }
}
using bART_Solutions_task.Core.Dto;
using bART_Solutions_task.Core.Services.Interfaces;
using FluentValidation;

namespace bART_Solutions_task.Core.FluentValidation;

public class UpdateAccountDtoValidator : AbstractValidator<UpdateAccountDto>
{
    private readonly IAccountService _accountService;
    public UpdateAccountDtoValidator(IAccountService accountService)
    {
        _accountService = accountService;
        
        RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Name).MustAsync(async (x, _) =>
        {
            bool result = await _accountService.IsNameInSystem(x);
            return result;
        }).WithMessage("NotFound").WithErrorCode("404");
    }
}
using bART_Solutions_task.Core.Dto;
using bART_Solutions_task.Core.Services.Interfaces;
using FluentValidation;

namespace bART_Solutions_task.Core.FluentValidation;

public class UpdateIncidentDtoValidator : AbstractValidator<UpdateIncidentDto>
{
    private readonly IIncidentService _incidentService;
    public UpdateIncidentDtoValidator(IIncidentService incidentService)
    {
        _incidentService = incidentService;

        RuleFor(x => x.Name).MustAsync(async (x, _) =>
        {
            bool result = await _incidentService.IsInSystem(x);
            return result;
        }).WithMessage("Incident not found");
        RuleFor(x => x.Description).NotEmpty().NotNull().WithMessage("Description is required");
    }
}
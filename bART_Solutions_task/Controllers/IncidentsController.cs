using bART_Solutions_task.Core.Dto;
using bART_Solutions_task.Core.FluentValidation;
using bART_Solutions_task.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace bART_Solutions_task.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class IncidentsController : ControllerBase
{
    private readonly IIncidentService _incidentService;

    public IncidentsController(IIncidentService incidentService)
    {
        _incidentService = incidentService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllIncidentsAsync()
    {
        return Ok(await _incidentService.GetAllIncidentsAsync());
    }

    [HttpGet("{name}")]
    public async Task<IActionResult> GetIncidentByNameAsync(string name)
    {
        var incident = await _incidentService.GetIncidentByNameAsync(name);
        if (incident is null)
        {
            return NotFound();
        }
        return Ok(incident);
    }

    [HttpPost]
    public async Task<IActionResult> AddIncidentAsync(CreateIncidentDto incident)
    {
        CreateIncidentDtoValidator validator = new CreateIncidentDtoValidator();
        var validationResult = await validator.ValidateAsync(incident);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }
        
        var result = await _incidentService.AddIncidentAsync(incident);
        if (result is null)
        {
            return BadRequest();
        }
        
        return Ok(result);
    }

    [HttpPost("addAccount")]
    public async Task<IActionResult> AddAccountAsync(string name, CreateAccountDto account)
    {
        CreateAccountDtoValidator validator = new CreateAccountDtoValidator();
        var validationResult = await validator.ValidateAsync(account);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        var result = await _incidentService.AddAccountAsync(name, account);
        if (result is null)
        {
            return BadRequest();
        }
        return Ok(result);
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateIncidentAsync(UpdateIncidentDto incident)
    {
        UpdateIncidentDtoValidator validator = new UpdateIncidentDtoValidator(_incidentService);
        var validationResult = await validator.ValidateAsync(incident);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        var result = await _incidentService.UpdateIncidentAsync(incident);
        if (result is null)
        {
            return BadRequest();
        }
        return Ok(result);
    }

}
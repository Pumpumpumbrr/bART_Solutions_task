using bART_Solutions_task.Core.Dto;
using bART_Solutions_task.Core.FluentValidation;
using bART_Solutions_task.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace bART_Solutions_task.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ContactsController : ControllerBase
{
    private readonly IContactService _contactService;

    public ContactsController(IContactService contactService)
    {
        _contactService = contactService;
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var result = await _contactService.GetContactByIdAsync(id);
        if (result is null)
        {
            return NotFound();
        }
        return Ok(result);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        return Ok(await _contactService.GetAllContactsAsync());
    }

    [HttpPut]
    public async Task<IActionResult> UpdateContactAsync(UpdateContactDto contact, int? accountId)
    {
        UpdateContactDtoValidator validator = new UpdateContactDtoValidator(_contactService);
        var validationResult = await validator.ValidateAsync(contact);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }
        var result = await _contactService.UpdateContactAsync(contact, accountId);
        if (result is null)
        {
            return BadRequest();
        }
        return Ok(result);
    }
}
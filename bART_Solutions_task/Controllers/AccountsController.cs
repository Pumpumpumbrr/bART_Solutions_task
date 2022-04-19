using bART_Solutions_task.Core.Dto;
using bART_Solutions_task.Core.FluentValidation;
using bART_Solutions_task.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace bART_Solutions_task.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountsController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountsController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAccountsAsync()
    {
        return Ok(await _accountService.GetAllAccountsAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAccountByIdAsync(int id)
    {
        var account = await _accountService.GetAccountByIdAsync(id);

        if (account is null)
        {
            return NotFound();
        }

        return Ok(account);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAccountAsync(UpdateAccountDto account, string? newName)
    {
        UpdateAccountDtoValidator validator = new UpdateAccountDtoValidator(_accountService);
        var validationResult = await validator.ValidateAsync(account);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        var result = await _accountService.UpdateAccountAsync(account, newName);
        if (result is null)
        {
            return NotFound();
        }
        return Ok(result);
    }

    [HttpPost("addContact")]
    public async Task<IActionResult> AddContactAsync(string name, CreateContactDto contact)
    {
        CreateContactDtoValidator validator = new CreateContactDtoValidator();
        var validationResult = await validator.ValidateAsync(contact);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        var result = await _accountService.AddContactAsync(name, contact);
        
        if (result is null)
        {
            return BadRequest();
        }
        return Ok(result);
    }
}
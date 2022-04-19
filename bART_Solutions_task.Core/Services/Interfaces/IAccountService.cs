using bART_Solutions_task.Core.Dto;
using bART_Solutions_task.Data.Models;

namespace bART_Solutions_task.Core.Services.Interfaces;

public interface IAccountService
{
    Task<IEnumerable<Account>> GetAllAccountsAsync();
    Task<Account?> GetAccountByIdAsync(int id);
    Task<Account> AddAccountAsync(CreateAccountDto account, string? incidentName);
    Task<Account> AddContactAsync(string name, CreateContactDto contact);
    Task<Account> UpdateAccountAsync(UpdateAccountDto account, string? newName);
    Task<bool> IsNameInSystem(string name);
}
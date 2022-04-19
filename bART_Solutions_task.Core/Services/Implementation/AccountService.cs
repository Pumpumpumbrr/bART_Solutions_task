using bART_Solutions_task.Core.Dto;
using bART_Solutions_task.Core.Services.Interfaces;
using bART_Solutions_task.Data.Context;
using bART_Solutions_task.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace bART_Solutions_task.Core.Services.Implementation;

public class AccountService : IAccountService
{
    private readonly ApplicationContext _context;
    private readonly IContactService _contactService;

    public AccountService(ApplicationContext context, IContactService contactService)
    {
        _context = context;
        _contactService = contactService;
    }

    public async Task<Account> AddAccountAsync(CreateAccountDto account, string? incidentName)
    {
        var contact = await _context.Contacts.FirstOrDefaultAsync(c => c.Email == account.Email);

        var newAccount = await _context.Accounts.FirstOrDefaultAsync(a => a.Name == account.Name);

        if (newAccount is not null)
        {
            return null;
        }

        newAccount = new Account()
        {
            Name = account.Name,
            IncidentName = incidentName
        };

        await _context.Accounts.AddAsync(newAccount);
        await _context.SaveChangesAsync();

        if (contact is null)
        {
            contact = await _contactService.AddContactAsync(
                new CreateContactDto
                {
                    FirstName = account.FirstName,
                    LastName = account.LastName,
                    Email = account.Email
                }, newAccount.Id);
        }
        else
        {
            contact = await _contactService.UpdateContactAsync(new UpdateContactDto()
            {
                Id = contact.Id,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                Email = contact.Email
            }, newAccount.Id);
        }

        newAccount.Contacts.Add(contact);
        
        await _context.SaveChangesAsync();
        
        return newAccount;
    }

    public async Task<Account> AddContactAsync(string name, CreateContactDto contact)
    {
        var account = await _context.Accounts.FirstOrDefaultAsync(a => a.Name == name);

        if (account is null)
        {
            return null;
        }

        var newContact = await _context.Contacts.FirstOrDefaultAsync(c => c.Email == contact.Email);

        if (newContact is not null)
        {
            return null;
        }

        newContact = await _contactService.AddContactAsync(contact, account?.Id);

        account?.Contacts.Add(newContact);
        
        await _context.SaveChangesAsync();

        return account;
    }

    public async Task<Account> UpdateAccountAsync(UpdateAccountDto account, string? newName)
    {
        var updateAccount = await _context.Accounts.FirstOrDefaultAsync(a => a.Id == account.Id);

        if (updateAccount is null)
        {
            return null;
        }

        updateAccount.Name = newName;

        await _context.SaveChangesAsync();

        return updateAccount;
    }

    public async Task<Account?> GetAccountByIdAsync(int id)
    {
        return await _context.Accounts.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Account>> GetAllAccountsAsync()
    {
        return await _context.Accounts.ToListAsync();
    }

    public async Task<bool> IsNameInSystem(string name)
    {
        return await _context.Accounts.AnyAsync(a => a.Name == name);
    }
}
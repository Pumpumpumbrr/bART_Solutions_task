using bART_Solutions_task.Core.Dto;
using bART_Solutions_task.Core.Services.Interfaces;
using bART_Solutions_task.Data.Context;
using bART_Solutions_task.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace bART_Solutions_task.Core.Services.Implementation;

public class ContactService : IContactService
{
    private readonly ApplicationContext _context;
    public ContactService(ApplicationContext context)
    {
        _context = context;
    }
    
    public async Task<Contact> AddContactAsync(CreateContactDto contact, int? accountId)
    {
        var newContact = new Contact
        {
            FirstName = contact.FirstName,
            LastName = contact.LastName,
            Email = contact.Email,
            AccountId = accountId
        };
        
        await _context.Contacts.AddAsync(newContact);
        await _context.SaveChangesAsync();
        
        return newContact;
    }

    public async Task<Contact> UpdateContactAsync(UpdateContactDto contact, int? accountId)
    {
        var updateContact = await _context.Contacts.FirstOrDefaultAsync(c => c.Id == contact.Id);
        
        if (updateContact is null)
        {
            return null;
        }
        updateContact.FirstName = contact.FirstName;
        updateContact.LastName = contact.LastName;
        updateContact.Email = contact.Email;
        updateContact.AccountId = accountId;
        await _context.SaveChangesAsync();
        
        return updateContact;
    }

    public async Task<IEnumerable<CreateContactDto>> GetAllContactsAsync()
    {
        return await _context.Contacts.Select(x => new CreateContactDto
        {
            FirstName = x.FirstName,
            LastName = x.LastName,
            Email = x.Email,
        }).ToListAsync();
    }

    public async Task<CreateContactDto?> GetContactByIdAsync(int id)
    {
        return await _context.Contacts.Where(x => x.Id == id).Select(x => new CreateContactDto
        {
            FirstName = x.FirstName,
            LastName = x.LastName,
            Email = x.Email,
        }).FirstOrDefaultAsync();
    }

    public async Task<bool> IsInSystem(string email)
    {
        return await _context.Contacts.AnyAsync(x => x.Email == email);
    }

}
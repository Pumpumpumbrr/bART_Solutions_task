using bART_Solutions_task.Core.Dto;
using bART_Solutions_task.Data.Models;

namespace bART_Solutions_task.Core.Services.Interfaces;

public interface IContactService
{
    Task<IEnumerable<CreateContactDto>> GetAllContactsAsync();
    Task<CreateContactDto?> GetContactByIdAsync(int id);
    Task<Contact> AddContactAsync(CreateContactDto contact, int? accountId);
    Task<Contact> UpdateContactAsync(UpdateContactDto contact, int? accountId);
    Task<bool> IsInSystem(string email);
}
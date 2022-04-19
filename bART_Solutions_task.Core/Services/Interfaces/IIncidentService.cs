using bART_Solutions_task.Core.Dto;
using bART_Solutions_task.Data.Models;

namespace bART_Solutions_task.Core.Services.Interfaces;

public interface IIncidentService
{
    Task<IEnumerable<Incident>> GetAllIncidentsAsync();
    Task<Incident?> GetIncidentByNameAsync(string name);
    Task<Incident> AddIncidentAsync(CreateIncidentDto incident);
    Task<Incident> AddAccountAsync(string name, CreateAccountDto account);
    Task<Incident> UpdateIncidentAsync(UpdateIncidentDto incident);
    Task<bool> IsInSystem(string name);
}
using bART_Solutions_task.Core.Dto;
using bART_Solutions_task.Core.Services.Interfaces;
using bART_Solutions_task.Data.Context;
using bART_Solutions_task.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace bART_Solutions_task.Core.Services.Implementation;

public class IncidentService : IIncidentService
{
    private readonly ApplicationContext _context;
    private readonly IAccountService _accountService;

    public IncidentService(ApplicationContext context, IAccountService accountService)
    {
        _context = context;
        _accountService = accountService;
    }

    public async Task<Incident> AddAccountAsync(string name, CreateAccountDto account)
    {
        var incident = await _context.Incidents.FirstOrDefaultAsync(i => i.Name == name);

        if (incident is null)
        {
            return null;
        }

        var newAccount = await _accountService.AddAccountAsync(account, incident.Name);

        incident.Accounts.Add(newAccount);
        await _context.SaveChangesAsync();

        return incident;
    }

    public async Task<Incident> AddIncidentAsync(CreateIncidentDto incident)
    {
        var newIncident = new Incident()
        {
            Description = incident.Description
        };

        await _context.Incidents.AddAsync(newIncident);
        await _context.SaveChangesAsync();

        var account = await _accountService.AddAccountAsync(
            new CreateAccountDto()
            {
                Name = incident.Name,
                FirstName = incident.FirstName,
                LastName = incident.LastName,
                Email = incident.Email
            }, newIncident.Name);

        newIncident.Accounts.Add(account);
        await _context.SaveChangesAsync();

        return newIncident;
    }

    public async Task<Incident> UpdateIncidentAsync(UpdateIncidentDto incident)
    {
        var updateIncident = await _context.Incidents.FirstOrDefaultAsync(i => i.Name == incident.Name);

        if (updateIncident is null)
        {
            return null;
        }

        updateIncident.Description = incident.Description;
        await _context.SaveChangesAsync();

        return updateIncident;
    }

    public async Task<IEnumerable<Incident>> GetAllIncidentsAsync()
    {
        return await _context.Incidents.ToListAsync();
    }

    public async Task<Incident?> GetIncidentByNameAsync(string name)
    {
        return await _context.Incidents.FirstOrDefaultAsync(x => x.Name == name);
    }

    public async Task<bool> IsInSystem(string name)
    {
        return await _context.Incidents.AnyAsync(i => i.Name == name);
    }

}
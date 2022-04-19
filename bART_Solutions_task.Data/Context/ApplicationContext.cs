using bART_Solutions_task.Data.ModelConfigurations;
using bART_Solutions_task.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace bART_Solutions_task.Data.Context;

public class ApplicationContext : DbContext
{
    public DbSet<Contact>? Contacts { get; set; }
    public DbSet<Account>? Accounts { get; set; }
    public DbSet<Incident>? Incidents { get; set; }

    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
        
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ContactConfiguration());
        modelBuilder.ApplyConfiguration(new AccountConfiguration());
        modelBuilder.ApplyConfiguration(new IncidentConfiguration());
    }
}


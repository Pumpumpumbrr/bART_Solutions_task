using bART_Solutions_task.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace bART_Solutions_task.Data.ModelConfigurations;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).ValueGeneratedOnAdd();

        builder.Property(a => a.Name).IsRequired().HasMaxLength(20);
        builder.HasIndex(a => a.Name).IsUnique();

        builder.HasOne(a => a.Incident)
            .WithMany(i => i.Accounts)
            .HasForeignKey(a => a.IncidentName)
            .HasPrincipalKey(i => i.Name)
            .IsRequired();
    }
}


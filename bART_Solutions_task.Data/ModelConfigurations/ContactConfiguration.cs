using bART_Solutions_task.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace bART_Solutions_task.Data.ModelConfigurations;

public class ContactConfiguration : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).ValueGeneratedOnAdd();

        builder.Property(c => c.FirstName).IsRequired().HasMaxLength(20);
        builder.Property(c => c.LastName).IsRequired().HasMaxLength(20);

        builder.Property(c => c.Email).IsRequired();
        builder.HasIndex(c => c.Email).IsUnique();

        builder.HasOne(c => c.Account)
            .WithMany(a => a.Contacts)
            .HasForeignKey(c => c.AccountId)
            .HasPrincipalKey(a => a.Id)
            .IsRequired();
    }
}
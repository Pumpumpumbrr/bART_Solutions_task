using bART_Solutions_task.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace bART_Solutions_task.Data.ModelConfigurations;

public class IncidentConfiguration : IEntityTypeConfiguration<Incident>
{
    public void Configure(EntityTypeBuilder<Incident> builder)
    {
        builder.HasKey(i => i.Name);
        builder.Property(i => i.Name).HasDefaultValueSql("NEWID()");

        builder.Property(i => i.Description).IsRequired().HasMaxLength(100);
    }
}


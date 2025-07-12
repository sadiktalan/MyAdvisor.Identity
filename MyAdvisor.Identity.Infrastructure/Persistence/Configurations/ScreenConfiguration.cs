using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyAdvisor.Identity.Domain.Entities;

namespace MyAdvisor.Identity.Infrastructure.Persistence.Configurations;

public class ScreenConfiguration : IEntityTypeConfiguration<Screen>
{
    public void Configure(EntityTypeBuilder<Screen> builder)
    {
        builder.Property(u => u.Name).IsRequired(true).HasMaxLength(100);
        builder.Property(u => u.Module).IsRequired(true).HasMaxLength(100);
    }
}
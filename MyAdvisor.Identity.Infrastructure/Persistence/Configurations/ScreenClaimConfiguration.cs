using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyAdvisor.Identity.Domain.Entities;

namespace MyAdvisor.Identity.Infrastructure.Persistence.Configurations;

public class ScreenClaimConfiguration : IEntityTypeConfiguration<ScreenClaim>
{
    public void Configure(EntityTypeBuilder<ScreenClaim> builder)
    {

    }
}
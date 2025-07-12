using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyAdvisor.Identity.Domain.Entities;

namespace MyAdvisor.Identity.Infrastructure.Persistence.Configurations;

public class UserSessionConfiguration
{
    public void Configure(EntityTypeBuilder<UserSession> builder)
    {
        builder.HasIndex(u => u.UserId);
        builder.HasIndex(u => u.RefreshTokenExpiration);

    }
}
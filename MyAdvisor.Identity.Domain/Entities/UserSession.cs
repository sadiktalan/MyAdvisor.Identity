using MyAdvisor.Identity.Domain.CommonModels;

namespace MyAdvisor.Identity.Domain.Entities;

public class UserSession : AuditEntity
{
    public Guid UserId { get; set; }
    public string RefreshToken { get; set; }
    public DateTime RefreshTokenExpiration { get; set; }
}
using MyAdvisor.Identity.Application.Commons;

namespace MyAdvisor.Identity.Domain.Entities;

public class ScreenClaim : AuditEntity
{
    public Guid ScreenId { get; set; }
    public string ClaimValue { get; set; }
    public Screen Screen { get; set; }
}
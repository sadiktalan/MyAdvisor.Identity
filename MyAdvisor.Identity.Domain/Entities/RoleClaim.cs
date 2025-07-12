using Microsoft.AspNetCore.Identity;
using MyAdvisor.Identity.Domain.Enums;

namespace MyAdvisor.Identity.Domain.Entities;

public class RoleClaim : IdentityRoleClaim<Guid>
{
    public DateTime CreateDate { get; set; }
    public DateTime UpdateDate { get; set; }
    public string LastModifiedBy { get; set; }
    public string CreatedBy { get; set; }
    public RecordStatus RecordStatus { get; set; }
}
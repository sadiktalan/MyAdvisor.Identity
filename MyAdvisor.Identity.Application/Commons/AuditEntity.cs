namespace MyAdvisor.Identity.Application.Commons;

public class AuditEntity
{
    public DateTime CreateDate { get; set; }
    public DateTime UpdateDate { get; set; }
    public string LastModifiedBy { get; set; }
    public string CreatedBy { get; set; }
}
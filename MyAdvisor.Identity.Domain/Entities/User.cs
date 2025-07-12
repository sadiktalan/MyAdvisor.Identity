using Microsoft.AspNetCore.Identity;
using MyAdvisor.Identity.Domain.Enums;

namespace MyAdvisor.Identity.Domain.Entities;

public class User : IdentityUser<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string IdentityNumber { get; set; }
    public DateTime BirthDate { get; set; }
    public UserType UserType { get; set; }
    public UserStatus UserStatus { get; set; }
    public string PhoneCode { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime UpdateDate { get; set; }
    public string LastModifiedBy { get; set; }
    public string CreatedBy { get; set; }
    public DateTime PasswordModifiedDate { get; set; }
    // public Guid? LoginLastActivityId { get; set; }
    public override string ToString()
    {
        return $"{FirstName} {LastName}";
    }
    public List<Role> Roles { get; set; }
}
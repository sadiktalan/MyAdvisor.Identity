using MyAdvisor.Identity.Domain.Enums;

namespace MyAdvisor.Identity.Application.Features.Queries;

public class UserDto
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string PhoneCode { get; set; }
    public string PhoneNumber { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public UserType UserType { get; set; }
    public string IdentityNumber { get; set; }
    public UserStatus UserStatus { get; set; }
    public DateTime PasswordModifiedDate { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime LockoutEnd { get; set; }
}
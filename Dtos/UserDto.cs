using abaBackOffice.DTOs;

public class UserDto : AuditableDto
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public bool IsVerified { get; set; }
    public bool IsAdmin { get; set; }
}
using MoneyManagement.Domain.Commons;
using MoneyManagement.Domain.Enums;

namespace MoneyManagement.Domain.Entities;

public class User : Auditable
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public double Wallet { get; set; }
    public UserRole Role { get; set; } = UserRole.User;
}

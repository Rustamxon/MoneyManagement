using MoneyManagement.Domain.Commons;

namespace MoneyManagement.Domain.Entities.Users;

public class UserImage : Auditable
{
    public string Name { get; set; }
    public string Path { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
}

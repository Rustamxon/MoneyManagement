using MoneyManagement.Domain.Commons;

namespace MoneyManagement.Domain.Entities;

public class Expense : Auditable
{
    public string Name { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }
    
    public double Value { get; set; }
}

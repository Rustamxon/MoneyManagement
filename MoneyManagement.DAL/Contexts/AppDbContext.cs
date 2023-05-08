using Microsoft.EntityFrameworkCore;
using MoneyManagement.Domain.Entities;
using MoneyManagement.Domain.Entities.Users;

namespace MoneyManagement.DAL.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<UserImage> UserImages { get; set; }
 
}

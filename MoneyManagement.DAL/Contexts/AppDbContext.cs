using Microsoft.EntityFrameworkCore;
using MoneyManagement.Domain.Entities;

namespace MoneyManagement.DAL.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Expense> Expenses { get; set; }

}

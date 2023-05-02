using Microsoft.EntityFrameworkCore;
using MoneyManagement.DAL.Contexts;

namespace MoneyManagement.Api.Extensions;

public static class DataExtension
{
    /// <summary>
    /// Automatically updated database based on latest migration
    /// </summary>
    /// <param name="app"></param>
    public static void ApplyMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        db.Database.Migrate();
    }
}

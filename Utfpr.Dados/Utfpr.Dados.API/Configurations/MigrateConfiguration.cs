using Microsoft.EntityFrameworkCore;

namespace Utfpr.Dados.API.Configurations;

public static class MigrateConfiguration
{
    public static async Task MigrateDatabase<T>(this WebApplication app) where T : DbContext
    {
        using(var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                var db = services.GetRequiredService<T>();
                
                var pendingMigrations = await db.Database.GetPendingMigrationsAsync();

                if (pendingMigrations.Any())
                {
                    Console.WriteLine($"You have {pendingMigrations.Count()} pending migrations to apply.");
                    Console.WriteLine("Applying pending migrations now");
                    await db.Database.MigrateAsync();
                }
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred while migrating the database.");
            }
        }
    }
}
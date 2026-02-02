using Microsoft.EntityFrameworkCore;
using UserManagementTask.Core.Entities;

namespace UserManagementTask.Infrastructure.Data
{
    public static class DataSeeder
    {
        // Define GUIDs as constants for consistent seeding
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            // Ensure database is created
            await context.Database.MigrateAsync();

            // ✅ Seed Ussrs
            if (!context.Users.Any())
            {
                // insert new dta
                var User = new User
                {
                    Username = "admin",
                    Password = "admin123",
                    UserFullName = "Aly Hany",
                    IsActive = true,
                    DateOfBirth = new DateTime(1995, 1, 1),
                    CreationDate = DateTime.Now
                };
                await context.Users.AddAsync(User);

                await context.SaveChangesAsync();
            }
        }

    }
}

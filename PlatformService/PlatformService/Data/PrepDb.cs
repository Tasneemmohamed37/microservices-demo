using Microsoft.EntityFrameworkCore;
using PlatformService.Models;

namespace PlatformService.Data
{

    // all seeding classes are static as we don't want to create an instance from it 
    public static class PrepDb
    {

        // IApplicationBuilder used to access app centeral DI
        public static void PrepPopulation(IApplicationBuilder app, bool isProd)
        {
            // using to dispose scope
            using(var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>(),isProd); 
            }
        }

        private static void SeedData(AppDbContext context, bool isProd)
        {
            if (isProd)
            {
                try
                {
                    Console.WriteLine("--> Applying Migration...");
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"couldn't run migrations: {ex.Message}");
                }
            }

            if (!context.Platforms.Any())
            {
                Console.WriteLine("--> Seeding Data...");

                context.Platforms.AddRange(
                    new Platform() { Name = "Dot Net", Publisher = "Microsoft", Cost = "Free" },
                    new Platform() { Name = "SQL Server Express", Publisher = "Microsoft", Cost = "Free" },
                    new Platform() { Name = "Kubernetes", Publisher = "Cloud Native Computing Foundation", Cost = "Free" }
                );

                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("--> We already have data");
            }
        }
        
    }

}



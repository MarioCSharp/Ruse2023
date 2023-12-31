﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Ruse2023.Data;
using Ruse2023.Data.Models;

namespace Ruse2023.Extensions
{
    public static class SampleData
    {
        public static async void Initialize(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var services = serviceScope.ServiceProvider;

            SeedAdministrator(services);
            MigrateDatabase(services);
        }

        private static void MigrateDatabase(IServiceProvider services)
        {
            var data = services.GetRequiredService<ApplicationDbContext>();
            data.Database.Migrate();
        }

        public static void SeedAdministrator(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<User>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            Task
                .Run(async () =>
                {
                    if (await roleManager.RoleExistsAsync("Administrator"))
                    {
                        return;
                    }

                    var admin = new IdentityRole("Administrator");
                    var moderator = new IdentityRole("Moderator");

                    await roleManager.CreateAsync(admin);
                    await roleManager.CreateAsync(moderator);

                    const string adminEmail = "mario_petkov2007@abv.bg";
                    const string adminPassword = "Administrator?123";

                    var user = new User
                    {
                        Email = adminEmail,
                        FirstName = "Mario",
                        LastName = "Petkov",
                        UserName = adminEmail,
                        EmailConfirmed = true,
                        PhoneNumber = "+359977725272",
                        BirthDate = DateTime.Now
                    };

                    await userManager.CreateAsync(user, adminPassword);
                    await userManager.AddToRoleAsync(user, admin.Name);
                })
                .GetAwaiter()
                .GetResult();
        }
    }
}

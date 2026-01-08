using Microsoft.AspNetCore.Identity;

namespace Partys.Data
{
    public static class IdentitySeeder
    {
        public static async Task SeedAsync(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();

            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

            var adminRoleName = "Admin";
            var adminEmail = "admin@admin.com";
            var adminPassword = "Admin123!";

            // Ensure Admin role exists
            if (!await roleManager.RoleExistsAsync(adminRoleName))
                await roleManager.CreateAsync(new IdentityRole(adminRoleName));

            // Ensure Admin user exists
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                adminUser = new IdentityUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);

                if (result.Succeeded)
                    await userManager.AddToRoleAsync(adminUser, adminRoleName);
            }
            else
            {
                // Ensure user is in Admin role
                if (!await userManager.IsInRoleAsync(adminUser, adminRoleName))
                    await userManager.AddToRoleAsync(adminUser, adminRoleName);
            }
        }
    }
}

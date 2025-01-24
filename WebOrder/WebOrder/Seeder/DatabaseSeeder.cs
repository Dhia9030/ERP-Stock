using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

public class DatabaseSeeder
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        // Check if the role 'SalesManager' exists, if not, create it
        var role = await roleManager.FindByNameAsync("SalesManager");
        if (role == null)
        {
            role = new IdentityRole("SalesManager");
            var roleResult = await roleManager.CreateAsync(role);
            if (roleResult.Succeeded)
            {
                Console.WriteLine("Role 'SalesManager' created successfully!");
            }
            else
            {
                Console.WriteLine("Error creating role:");
                foreach (var error in roleResult.Errors)
                {
                    Console.WriteLine($"- {error.Description}");
                }
            }
        }

        // Check if the user already exists
        var existingUser = await userManager.FindByEmailAsync("sahbi@gmail.com");
        if (existingUser == null)
        {
            // Create a new user
            var user = new IdentityUser
            {
                UserName = "sahbi",
                Email = "sahbi@gmail.com",
                EmailConfirmed = true // Set to true if you want the email already confirmed
            };

            // Set the password
            var result = await userManager.CreateAsync(user, "Sahbi2003*");

            if (result.Succeeded)
            {
                Console.WriteLine("User seeded successfully!");

                // Assign the 'SalesManager' role to the user
                var addToRoleResult = await userManager.AddToRoleAsync(user, "SalesManager");
                if (addToRoleResult.Succeeded)
                {
                    Console.WriteLine("User assigned to 'SalesManager' role successfully!");
                }
                else
                {
                    Console.WriteLine("Error assigning role:");
                    foreach (var error in addToRoleResult.Errors)
                    {
                        Console.WriteLine($"- {error.Description}");
                    }
                }
            }
            else
            {
                Console.WriteLine("Error seeding user:");
                foreach (var error in result.Errors)
                {
                    Console.WriteLine($"- {error.Description}");
                }
            }
        }
        else
        {
            Console.WriteLine("User already exists.");
        }
    }
}

﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace BookStore_API.Data
{
    public static class SeedData
    {
        public async static Task Seed(UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            await SeedRoles(roleManager);
            await SeedUsers(userManager);
        }

        private async static Task SeedUsers(UserManager<IdentityUser> userManager)
        {
            if (await userManager.FindByEmailAsync("admin@bookstore.com") == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "admin@bookstore.com",
                    Email = "admin@bookstore.com"
                };

                IdentityResult result = await userManager.CreateAsync(user, "P@ssw0rd");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Administrator");
                }
            }

            if (await userManager.FindByEmailAsync("customer1@gmail.com") == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "customer1@gmail.com",
                    Email = "customer1@gmail.com"
                };

                IdentityResult result = await userManager.CreateAsync(user, "P@ssw0rd");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Customer");
                }
            }

            if (await userManager.FindByEmailAsync("customer2@gmail.com") == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "customer2@gmail.com",
                    Email = "customer2@gmail.com"
                };

                IdentityResult result = await userManager.CreateAsync(user, "P@ssw0rd");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Customer");
                }
            }
        }

        private async static Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync("Administrator"))
            {
                IdentityRole role = new IdentityRole
                {
                    Name = "Administrator"
                };

                await roleManager.CreateAsync(role);
            }

            if (!await roleManager.RoleExistsAsync("Customer"))
            {
                IdentityRole role = new IdentityRole
                {
                    Name = "Customer"
                };

                await roleManager.CreateAsync(role);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Database.Data
{
    public class UserSeeding
    {
        public static async Task Seed(UserManager<ApplicationUser> userManager)
        {
            string adminPassword = "NewAdminPassword@123";
            ApplicationUser adminUser = new()
            {
                Email = "Admin123@admin.com",
                UserName = "mahmoudshakourali",
                EmailConfirmed = true,
                PhoneNumber = "01062591395",
            };

            var admin = await userManager.CreateAsync(adminUser, adminPassword);

            if (admin.Succeeded)
                await userManager.AddToRoleAsync(adminUser, "Admin");

        }
    }
}
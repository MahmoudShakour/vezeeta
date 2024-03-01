using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Database.Data
{
    public class UserRolesSeeding
    {
        public static async Task Seed(RoleManager<IdentityRole> roleManager)
        {
            if (await roleManager.FindByNameAsync("Admin") == null)
                await roleManager.CreateAsync(new IdentityRole { Name = "Admin" });

            if (await roleManager.FindByNameAsync("Doctor") == null)
                await roleManager.CreateAsync(new IdentityRole { Name = "Doctor" });

            if (await roleManager.FindByNameAsync("Patient") == null)
                await roleManager.CreateAsync(new IdentityRole { Name = "Patient" });
            
        }
    }
}
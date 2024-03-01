using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models;
using Infrastructure.Database.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Database
{
    public class DbSeeding
    {
        public static async Task Seed(IApplicationBuilder applicationBuilder)
        {
            using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();
            var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

            var roleManager = serviceScope.ServiceProvider.GetRequiredService<
               RoleManager<IdentityRole>
           >();
            var userManager = serviceScope.ServiceProvider.GetRequiredService<
                UserManager<ApplicationUser>
            >();

            await SpecializationSeeding.Seed(applicationBuilder);
            await UserSeeding.Seed(userManager);
            await UserRolesSeeding.Seed(roleManager);

        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Enums;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Database.Data
{
    public static class SpecializationSeeding
    {
        public static async Task Seed(IApplicationBuilder applicationBuilder)
        {
            using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();
            var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

            if (context == null)
                return;

            if (!context.Specializations.Any())
            {
                foreach (Specialization specialization in Enum.GetValues(typeof(Specialization)))
                {
                    await context.Specializations.AddAsync(
                        new Core.Models.Specialization { Name = specialization.ToString() }
                    );
                }

                await context.SaveChangesAsync();
            }
        }
    }
}
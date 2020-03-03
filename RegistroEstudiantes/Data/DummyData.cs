
using Microsoft.AspNetCore.Identity;
using RegistroEstudiantes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistroEstudiantes.Data
{
    public class DummyData
    {
        public static async Task Initialize(ApplicationDbContext context,
                                                UserManager<AplicationUser> userManager,
                                                RoleManager<AplicationRole> roleManager)
        {
            context.Database.EnsureCreated();
            string[] roleNames = { "admin", "estudiante", "docenete" };
            foreach (var roleName in roleNames)
            {
                if (!roleManager.RoleExistsAsync(roleName).Result) 
                {
                    AplicationRole role = new AplicationRole
                    {
                        Name = roleName,
                        NormalizedName = roleName
                    };
                    roleManager.CreateAsync(role).Wait();
                }
            }

            if (await userManager.FindByEmailAsync("admin@gmail.com") == null)
            {
                AplicationUser user = new AplicationUser
                {
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                    EmailConfirmed = true,
                    LockoutEnabled = false

                };

                IdentityResult result = userManager.CreateAsync(user, "Admin1234$").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "admin").Wait();
                }
            }
        }
    }
}

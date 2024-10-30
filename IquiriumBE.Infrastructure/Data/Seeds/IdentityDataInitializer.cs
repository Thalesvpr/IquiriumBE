using IquiriumBE.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace IquiriumBE.Infrastructure.Data.Seeds
{
    public static class IdentityDataInitializer
    {
        public static async Task SeedRoles(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var roleManager = scopedServices.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = scopedServices.GetRequiredService<UserManager<AccountEntity>>();

                // Criar as roles "Admin" e "User" se elas não existirem
                string[] roleNames = { "Admin", "User" };
                foreach (var roleName in roleNames)
                {
                    if (!await roleManager.RoleExistsAsync(roleName))
                    {
                        await roleManager.CreateAsync(new IdentityRole(roleName));
                    }
                }

                // Criar um usuário Admin padrão
                var adminEmail = "admin@example.com";
                var adminPassword = "Admin@123"; // Troque para algo mais seguro em produção

                if (await userManager.FindByEmailAsync(adminEmail) == null)
                {
                    var adminUser = new AccountEntity
                    {
                        UserName = adminEmail,
                        Email = adminEmail,
                        EmailConfirmed = true
                    };

                    var result = await userManager.CreateAsync(adminUser, adminPassword);
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(adminUser, "Admin");
                    }
                }
            }
        }
    }
}

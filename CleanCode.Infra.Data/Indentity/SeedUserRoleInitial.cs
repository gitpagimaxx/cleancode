using CleanCode.Domain.Account;
using Microsoft.AspNetCore.Identity;

namespace CleanCode.Infra.Data.Indentity;

public class SeedUserRoleInitial(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager) : ISeedUserRoleInitial
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly RoleManager<IdentityRole> _roleManager = roleManager;

    public async void SeedRoles()
    {
        if (!_roleManager.RoleExistsAsync("Admin").Result)
            await _roleManager.CreateAsync(new() { Name = "Admin", NormalizedName = "ADMIN" });

        if (!_roleManager.RoleExistsAsync("User").Result)
            await _roleManager.CreateAsync(new() { Name = "User", NormalizedName = "USER" });
    }

    public async void SeedUsers()
    {
        if (_userManager.FindByEmailAsync("usuario@localhost").Result == null)
        {
            ApplicationUser user = new()
            {
                UserName = "usuario@localhost",
                Email = "usuario@localhost",
                EmailConfirmed = true,
                NormalizedUserName = "USUARIO@LOCALHOST",
                NormalizedEmail = "USUARIO@LOCALHOST",
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            IdentityResult result = _userManager.CreateAsync(user, "Numsey@123").Result;
            
            if (result.Succeeded)
            {
                _userManager.AddToRoleAsync(user, "User").Wait();
            }
        }

        if (_userManager.FindByEmailAsync("admin@localhost").Result == null)
        {
            ApplicationUser user = new()
            {
                UserName = "admin@localhost",
                Email = "admin@localhost",
                EmailConfirmed = true,
                NormalizedUserName = "ADMIN@LOCALHOST",
                NormalizedEmail = "ADMIN@LOCALHOST",
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            IdentityResult result = _userManager.CreateAsync(user, "Numsey@123").Result;

            if (result.Succeeded)
            {
                _userManager.AddToRoleAsync(user, "User").Wait();
            }
        }
    }
}

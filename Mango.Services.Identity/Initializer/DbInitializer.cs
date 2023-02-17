using IdentityModel;
using Mango.Services.Identity.DbContexts;
using Mango.Services.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Mango.Services.Identity.Initializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(ApplicationDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public void Initialize()
        {
            var role = _roleManager.FindByNameAsync(SD.Admin).Result;
            if (role == null)
            {
                _roleManager.CreateAsync(new IdentityRole(SD.Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Customer)).GetAwaiter().GetResult();
            }
            else { return; }

            var adminUser = new ApplicationUser()
            {
                UserName = "tclarkston",
                Email = "todd@wireservers.com",
                EmailConfirmed = true,
                PhoneNumber = "866.508.8066",
                FirstName = "Todd",
                LastName = "Clarkston"
            };

            var userResult = _userManager.CreateAsync(adminUser, "Admin123!").GetAwaiter().GetResult();
            if (!userResult.Succeeded)
            {
                foreach (IdentityError error in userResult.Errors)
                {
                    Console.WriteLine($"Oops! {error.Description} {error.Code}");
                }

                return;
            }

            var roleResult = _userManager.AddToRoleAsync(adminUser, SD.Admin).GetAwaiter().GetResult();
            if (!roleResult.Succeeded)
            {
                foreach (IdentityError error in roleResult.Errors)
                {
                    Console.WriteLine($"Oops! {error.Description} {error.Code}");
                }

                return;
            }
            var temp1 = _userManager.AddClaimsAsync(adminUser, new Claim[]
            {
                new Claim(JwtClaimTypes.Name, $"{adminUser.FirstName} {adminUser.LastName}"),
                new Claim(JwtClaimTypes.GivenName, adminUser.FirstName),
                new Claim(JwtClaimTypes.FamilyName, adminUser.LastName),
            }).Result;

            var customerUser = new ApplicationUser()
            {
                UserName = "customer@example.com",
                Email = "customer@example.com",
                EmailConfirmed = true,
                PhoneNumber = "1111111111",
                FirstName = "Ben",
                LastName = "Customer"
            };

            _userManager.CreateAsync(customerUser, "Customer123!").GetAwaiter().GetResult();
            _userManager.AddToRoleAsync(customerUser, SD.Admin).GetAwaiter().GetResult();

            var temp2 = _userManager.AddClaimsAsync(customerUser, new Claim[]
            {
                new Claim(JwtClaimTypes.Name, $"{customerUser.FirstName} {adminUser.LastName}"),
                new Claim(JwtClaimTypes.GivenName, customerUser.FirstName),
                new Claim(JwtClaimTypes.FamilyName, customerUser.LastName),
            }).Result;
        }
    }
}

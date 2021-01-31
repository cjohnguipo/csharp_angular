using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "Christian John Guipo",
                    Email = "cjohnguipo@gmail.com",
                    UserName = "cjohnguipo@gmail.com",
                    Address = new Address {
                        FirstName = "Christian John",
                        LastName = "Guipo",
                        Street = "Tokyo Street",
                        City = "General Santos",
                        State = "South Cotabato",
                        ZipCode = "9500"
                    }
                };

                await userManager.CreateAsync(user,"Pa$$w0rd");

            }
        }
    }
}
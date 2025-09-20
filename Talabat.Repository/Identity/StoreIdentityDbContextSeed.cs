using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.IdentityModule;

namespace Talabat.Repository.Identity
{
    public static class StoreIdentityDbContextSeed
    {
        public static async Task UserSeedAsync(UserManager<AppUser> userManager)
        {
            var user = new AppUser()
            {
                DisplayName = "Manar ElTayeb",
                Email = "manareltayeb825@gmail.com",
                UserName = "manareltayeb825",
                PhoneNumber = "01123456789",
            };

            await userManager.CreateAsync(user, "Pa$$w0rd");
        }
    }
}

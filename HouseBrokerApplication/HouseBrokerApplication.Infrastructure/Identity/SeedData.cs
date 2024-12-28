using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace HouseBrokerApplication.Infrastructure.Identity
{
    public class SeedData
    {
        public static async Task InitialzeAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            // seed roles 
            if(!await roleManager.RoleExistsAsync("Broker"))
            {
                await roleManager.CreateAsync(new IdentityRole("Broker"));
            }

            if(!await roleManager.RoleExistsAsync("Seeker"))
            {
                await roleManager.CreateAsync(new IdentityRole("Seeker"));
            }


            // Seed an admin user 
            var brokerEmail = "admin@example.com";
            var broker = await userManager.FindByEmailAsync(brokerEmail);

            if(broker == null)
            {
                var brokerUser = new IdentityUser
                {
                    UserName = brokerEmail,
                    Email = brokerEmail
                };

                await userManager.CreateAsync(brokerUser, "Broker@123");
                await userManager.AddToRoleAsync(brokerUser, "broker");
            }
        }
    }
}

using LogBilisim.Web.Models;
using Microsoft.AspNetCore.Identity;

namespace LogBilisim.Web.Data.DataSeed;

public static class UserSeeding
{
    public static void UseSeedUser(this WebApplication app)
    {
        var scope = app.Services.CreateScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

        if (!userManager.Users.Any())
        {
            userManager.CreateAsync(new User
            {
                UserName = "Admin",
                Email = "admin@testmail.com"
            }, password: "Admin123*").Wait();
        }
    }
}

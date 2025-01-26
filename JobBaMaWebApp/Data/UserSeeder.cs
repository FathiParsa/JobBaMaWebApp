using JobBaMaWebApp.Constants;
using Microsoft.AspNetCore.Identity;

public class UserSeeder
{
    public static async Task SeedUsersAsync(IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
        await CreateUserWithRoleAsync(userManager , "dayihidden@gmail.com" , "Parsa@123" , Roles.Admin);
        await CreateUserWithRoleAsync(userManager, "parsafathi.pf.2002@gmail.com", "Parsa@123", Roles.JobSeeker);
        await CreateUserWithRoleAsync(userManager, "Jobbama@gmail.com", "Parsa@123", Roles.Employer);
    }

    private static async Task CreateUserWithRoleAsync(
        UserManager<IdentityUser> userManager,
        string email , string password , string role)
    {
        if (await userManager.FindByEmailAsync(email) == null)
        {
            var user = new IdentityUser
            {
                Email = email,
                UserName = email,
                EmailConfirmed = true
            };
            var result = await userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, role);
            }
            else
            {
                throw new Exception($"Error : {String.Join(",", result.Errors)}");
            }
        }

    }

}

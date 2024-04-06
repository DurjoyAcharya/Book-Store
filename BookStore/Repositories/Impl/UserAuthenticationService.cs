using BookStore.Models.DTO;
using BookStore.Repositories.Abstract;
using Microsoft.AspNetCore.Identity;

namespace BookStore.Repositories.Impl;

public class UserAuthenticationService:IUserAuthenticationService
{

    private readonly UserManager<ApplicationUsers> UserManager;
    private readonly RoleManager<IdentityRole> RoleManager;
    private readonly SignInManager<ApplicationUsers> SignInManager;

    public UserAuthenticationService(UserManager<ApplicationUsers> userManager, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUsers> signInManager)
    {
        UserManager = userManager;
        RoleManager = roleManager;
        SignInManager = signInManager;
    }


    public Task<Status> LoginAsync(Login loginModel)
    {
        throw new NotImplementedException();
    }

    public Task LogOutAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Status> RegistrationAsync(Registration loginModel)
    {
        if (loginModel.Email != null)
        {
            var status = new Status(0,"");
            var userExists = await UserManager.FindByNameAsync(loginModel.Email);
            if (userExists!=null)
            {
                status = status with { StatusCode = 0 };
                status = status with { Notify = "This email already exist" };
                return status;
            }

            var users = new ApplicationUsers()
            {
                Email = loginModel.Email,
                Name = loginModel.FirstName+" "+loginModel.LastName,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = loginModel.Username,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            var result=await UserManager.CreateAsync()
        }
    }
}
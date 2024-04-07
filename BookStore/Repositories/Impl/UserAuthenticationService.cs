using System.Security.Claims;
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


    public async Task<Status> LoginAsync(Login loginModel)
    {
        var status = new Status(0, "Init");
        if (loginModel.Username != null)
        {
            var user = await UserManager.FindByNameAsync(loginModel.Username);
            if (user==null)
            {
                status = status with { StatusCode = 0 };
                status = status with { Notify = "Invalid Username" };
                return status;
            }

            if (loginModel.Password != null && !await UserManager.CheckPasswordAsync(user,loginModel.Password))
            {
                status = status with { StatusCode = 0 };
                status = status with { Notify = "Invalid Password" };
                return status;
            }
            var signInResult = await SignInManager.PasswordSignInAsync(user, loginModel.Password, false, true);
            if (signInResult.Succeeded)
            {
                var userRoles = await UserManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Name)
                };
                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role,userRole));
                }

                status = status with { StatusCode = 1 };
                status = status with { Notify = "Login Successfully" };
            }else if (signInResult.IsLockedOut)   
            {
                status = status with { StatusCode = 0 };
                status = status with { Notify = "Login LockOut" };
            }
            else
            {
                status = status with { StatusCode = 0 };
                status = status with { Notify = "Error In Logging..." };
            }
        }

        return status;
    }

    public async Task LogOutAsync()
    {
       await SignInManager.SignOutAsync();
    }

    public async Task<Status> RegistrationAsync(Registration loginModel)
    {
        var status = new Status(0,"Failed");
        if (loginModel.Email != null)
        {
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
            if (loginModel.Password != null)
            {
                var result = await UserManager.CreateAsync(users, loginModel.Password);
                if (!result.Succeeded)
                {
                    status = status with { StatusCode = 0 };
                    status = status with { Notify = "User Creation Failed" };
                    return status;
                }
            }

            if (loginModel.Role != null && ! await RoleManager.RoleExistsAsync(loginModel.Role))
            {
                await RoleManager.CreateAsync(new IdentityRole(loginModel.Role));
            }

            status = status with { StatusCode = 1 };
            status = status with { Notify = "User Create Successfully" };
        }

        return status;
    }
}
using BookStore.Models.DTO;

namespace BookStore.Repositories.Abstract
{
    interface IUserAuthenticationService
    {
        Task<Status> LoginAsync(Login loginModel);
        Task LogOutAsync();
        Task<Status> RegistrationAsync(Registration loginModel);

        // Task<Status> ForgotByUsernameAsync(Login loginModel, string Username);
        // Task<Status> ForgotByEmailAsync(Login loginModel, string Email);
    }
}
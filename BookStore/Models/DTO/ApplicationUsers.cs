using Microsoft.AspNetCore.Identity;

namespace BookStore.Models.DTO;

public class ApplicationUsers:IdentityUser
{
    public string Name { get; set; } = null!;
}
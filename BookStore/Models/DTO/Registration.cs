using System.ComponentModel.DataAnnotations;
using Microsoft.Identity.Client;

namespace BookStore.Models.DTO
{
    public class Registration
    {
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        public string? Username { get; set; }

        [Required]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&]) [A-Za-z\\d@$!%*?&]{8,}\n")]
        public string? Password { get; set; }

        [Required]
        [Compare("Password")]
        public string? ConfirmPassword { get; set; }

        public string? Role { get; set; }
    }
}
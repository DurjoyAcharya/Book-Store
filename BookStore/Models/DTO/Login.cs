using System.ComponentModel.DataAnnotations;


namespace BookStore.Models.DTO
{
    public class Login
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        public string? Username { get; set; }

        [Required]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&]) [A-Za-z\\d@$!%*?&]{8,}\n")]
        public string? Password { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace PhotoHub.Models
{
    public class UserModel
    {
        public Guid IdUser { get; set; }

        [StringLength(50, ErrorMessage = "String too long. (max 50 char.)")]
        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }

        [StringLength(100, ErrorMessage = "String too long. (max 100 char.)")]
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string PasswordHash { get; set; }

        [StringLength(20, ErrorMessage = "String too long. (max 20 char.)")]
        public string Role { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace PhotoHub.ViewModels
{
    public class UserViewModel
    {
        public Guid IdUser { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@@!%*?&-])[A-Za-z\d@@!%*?&-]{8,}$",
            ErrorMessage = "Password must be at least 8 characters long, include an uppercase letter, a lowercase letter, a number, and a special character.")]
        public string PasswordHash { get; set; }

        [Required(ErrorMessage = "Confirm Password is required.")]
        [DataType(DataType.Password)]
        [Compare("PasswordHash", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }
    }
}

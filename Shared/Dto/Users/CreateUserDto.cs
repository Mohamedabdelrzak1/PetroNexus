using System.ComponentModel.DataAnnotations;

namespace Shared.Dto.Users
{
    public class CreateUserDto
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string DisplayName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(150)]
        public string Email { get; set; } = string.Empty;

        [Phone]
        [StringLength(20)]
        public string? PhoneNumber { get; set; }

        // ✅ Password قوية
        [Required]
        [StringLength(100, MinimumLength = 8)]
        [RegularExpression(
            @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).+$",
            ErrorMessage = "Password must contain uppercase, lowercase, number, and special character.")]
        public string Password { get; set; } = string.Empty;

        // ✅ Role محددة فقط
        [Required]
        [RegularExpression(
            "^(Manager|Accounting|Hr)$",
            ErrorMessage = "Invalid role.")]
        public string Role { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;
    }
}

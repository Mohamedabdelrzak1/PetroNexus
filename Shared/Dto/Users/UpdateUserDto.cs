using System.ComponentModel.DataAnnotations;

namespace Shared.Dto.Users
{
    public class UpdateUserDto
    {
        [StringLength(100, MinimumLength = 2)]
        public string? DisplayName { get; set; }

        [EmailAddress]
        [StringLength(150)]
        public string? Email { get; set; }

        [Phone]
        [StringLength(20)]
        public string? PhoneNumber { get; set; }

        // ✅ لو المستخدم غير الباسورد لازم تبقى قوية
        [StringLength(100, MinimumLength = 8)]
        [RegularExpression(
            @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).+$",
            ErrorMessage = "Password must contain uppercase, lowercase, number, and special character.")]
        public string? Password { get; set; }

        // ✅ رول محددة فقط
        [RegularExpression(
            "^(Manager|Accounting|Hr)$",
            ErrorMessage = "Invalid role.")]
        public string? Role { get; set; }

        // ✅ نخليها nullable عشان partial update
        public bool? IsActive { get; set; }
    }
}

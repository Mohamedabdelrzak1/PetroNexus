using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dto.Users
{
    public class UpdateMyProfileDto
    {
        [StringLength(100, MinimumLength = 2)]
        public string? DisplayName { get; set; }

        [Phone]
        public string? PhoneNumber { get; set; }

        [EmailAddress]
        public string? Email { get; set; }
    }
}

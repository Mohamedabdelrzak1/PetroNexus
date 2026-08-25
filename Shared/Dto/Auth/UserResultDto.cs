using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dto.Auth
{
    public class UserResultDto
    {
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public string Token { get; set; }

        public bool IsOnline { get; set; }
        public DateTime? LastLogin { get; set; }
        public DateTime? LastSeen { get; set; }

    }
}

using Domain.Common;

namespace Domain.Models
{
    public class PrincipalContact : BaseEntity<int>
    {
        public int PrincipalId { get; set; }
        public Principal Principal { get; set; }

        public string FullName { get; set; }
        public string? JobTitle { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
    }
}

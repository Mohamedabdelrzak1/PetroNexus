using Domain.Common;
using Domain.Enums;

namespace Domain.Models
{
    public class Account : BaseEntity<int>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public AccountType Type { get; set; }

        public int? ParentAccountId { get; set; }
        public Account? ParentAccount { get; set; }
    }
}

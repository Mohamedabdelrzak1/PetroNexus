using System;
using Domain.Common;

namespace Domain.Models
{
    public class EmployeeDocument : BaseEntity<int>
    {
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public string DocumentName { get; set; }
        public string FilePath { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }
}

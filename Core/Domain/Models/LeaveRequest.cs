using System;
using Domain.Common;
using Domain.Enums;

namespace Domain.Models
{
    public class LeaveRequest : BaseEntity<int>
    {
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public LeaveType Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string? Reason { get; set; }

        public bool IsApproved { get; set; } = false;
        public DateTime? ApprovedAt { get; set; }
    }
}

using System;
using Domain.Common;

namespace Domain.Models
{
    public class Attendance : BaseEntity<int>
    {
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan? CheckIn { get; set; }
        public TimeSpan? CheckOut { get; set; }

        public int LateMinutes { get; set; } = 0;
        public bool IsAbsent { get; set; } = false;
    }
}

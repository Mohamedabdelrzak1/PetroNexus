using System;
using Domain.Common;
using Domain.Enums;

namespace Domain.Models
{
    public class Salary : BaseEntity<int>
    {
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        // =========================
        // 💰 Base
        // =========================
        public decimal BasicSalary { get; set; }

        public decimal OvertimeAmount { get; set; }
        public decimal Bonus { get; set; }

        // =========================
        // ❌ Deductions
        // =========================
        public decimal LateDeduction { get; set; }
        public decimal AbsenceDeduction { get; set; }

        // =========================
        // 📊 Attendance تأثيره
        // =========================
        public int TotalLateMinutes { get; set; }
        public int TotalAbsenceDays { get; set; }

        // =========================
        // 💵 Final
        // =========================
        public decimal NetSalary { get; set; }

        public int Month { get; set; }
        public int Year { get; set; }

        // =========================
        // 🔥 Workflow
        // =========================
        public SalaryStatus Status { get; set; } = SalaryStatus.Draft;

        public DateTime? ApprovedAt { get; set; }
        public DateTime? PaidAt { get; set; }

        // =========================
        // 📒 Accounting
        // =========================
        public int? JournalEntryId { get; set; }
        public JournalEntry? JournalEntry { get; set; }

        // =========================
        // 🕒 Audit
        // =========================
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}

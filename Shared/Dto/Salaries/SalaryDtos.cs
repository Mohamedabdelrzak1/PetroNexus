using System;
using Domain.Enums;

namespace Shared.Dto.Salaries
{
    public class SalaryCreateDto
    {
        public int EmployeeId { get; set; }
        public decimal BasicSalary { get; set; }
        public decimal OvertimeAmount { get; set; }
        public decimal Bonus { get; set; }
        public decimal LateDeduction { get; set; }
        public decimal AbsenceDeduction { get; set; }
        public int TotalLateMinutes { get; set; }
        public int TotalAbsenceDays { get; set; }
        public decimal NetSalary { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public SalaryStatus Status { get; set; }
        public DateTime? ApprovedAt { get; set; }
        public DateTime? PaidAt { get; set; }
        public int? JournalEntryId { get; set; }
    }

    public class SalaryUpdateDto
    {
        public int EmployeeId { get; set; }
        public decimal BasicSalary { get; set; }
        public decimal OvertimeAmount { get; set; }
        public decimal Bonus { get; set; }
        public decimal LateDeduction { get; set; }
        public decimal AbsenceDeduction { get; set; }
        public int TotalLateMinutes { get; set; }
        public int TotalAbsenceDays { get; set; }
        public decimal NetSalary { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public SalaryStatus Status { get; set; }
        public DateTime? ApprovedAt { get; set; }
        public DateTime? PaidAt { get; set; }
        public int? JournalEntryId { get; set; }
    }

    public class SalaryResponseDto
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } = null!;
        public decimal BasicSalary { get; set; }
        public decimal OvertimeAmount { get; set; }
        public decimal Bonus { get; set; }
        public decimal LateDeduction { get; set; }
        public decimal AbsenceDeduction { get; set; }
        public int TotalLateMinutes { get; set; }
        public int TotalAbsenceDays { get; set; }
        public decimal NetSalary { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public SalaryStatus Status { get; set; }
        public DateTime? ApprovedAt { get; set; }
        public DateTime? PaidAt { get; set; }
        public int? JournalEntryId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

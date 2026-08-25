using System;
using Domain.Enums;

namespace Shared.Dto.LeaveRequests
{
    public class LeaveRequestCreateDto
    {
        public int EmployeeId { get; set; }
        public LeaveType Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Reason { get; set; }
        public bool IsApproved { get; set; }
        public DateTime? ApprovedAt { get; set; }
    }

    public class LeaveRequestUpdateDto
    {
        public int EmployeeId { get; set; }
        public LeaveType Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Reason { get; set; }
        public bool IsApproved { get; set; }
        public DateTime? ApprovedAt { get; set; }
    }

    public class LeaveRequestResponseDto
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } = null!;
        public LeaveType Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Reason { get; set; }
        public bool IsApproved { get; set; }
        public DateTime? ApprovedAt { get; set; }
    }
}

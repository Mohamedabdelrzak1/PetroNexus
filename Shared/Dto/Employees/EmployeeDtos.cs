using System;
using System.Collections.Generic;

namespace Shared.Dto.Employees
{
    public class EmployeeCreateDto
    {
        public string FullName { get; set; } = null!;
        public string JobTitle { get; set; } = null!;
        public int DepartmentId { get; set; }
        public string? AppUserId { get; set; }
        public DateTime HireDate { get; set; }
        public bool IsActive { get; set; }
    }

    public class EmployeeUpdateDto
    {
        public string FullName { get; set; } = null!;
        public string JobTitle { get; set; } = null!;
        public int DepartmentId { get; set; }
        public string? AppUserId { get; set; }
        public DateTime HireDate { get; set; }
        public bool IsActive { get; set; }
    }

    public class EmployeeResponseDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string JobTitle { get; set; } = null!;
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; } = null!;
        public string? AppUserId { get; set; }
        public DateTime HireDate { get; set; }
        public bool IsActive { get; set; }
    }

    public class EmployeeDetailsDto : EmployeeResponseDto
    {
        public List<Shared.Dto.Salaries.SalaryResponseDto> Salaries { get; set; } = new();
        public List<Shared.Dto.Attendances.AttendanceResponseDto> Attendances { get; set; } = new();
        public List<Shared.Dto.LeaveRequests.LeaveRequestResponseDto> LeaveRequests { get; set; } = new();
        public List<EmployeeDocumentResponseDto> Documents { get; set; } = new();
    }
}

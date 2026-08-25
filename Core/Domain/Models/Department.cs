using System;
using System.Collections.Generic;
using Domain.Common;

namespace Domain.Models
{
    public class Department : BaseEntity<int>
    {
        public string Name { get; set; }

        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }

    public class Employee : BaseEntity<int>
    {
        public string FullName { get; set; }
        public string JobTitle { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        // ربط اختياري بحساب دخول للنظام (لو الموظف مهندس موقع/مستخدم للنظام)
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }

        public DateTime HireDate { get; set; }
        public bool IsActive { get; set; } = true;

        public ICollection<Salary> Salaries { get; set; } = new List<Salary>();
        public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
        public ICollection<LeaveRequest> LeaveRequests { get; set; } = new List<LeaveRequest>();
        public ICollection<EmployeeDocument> Documents { get; set; } = new List<EmployeeDocument>();
    }
}

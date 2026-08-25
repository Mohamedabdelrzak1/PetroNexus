using System;

namespace Shared.Dto.Attendances
{
    public class AttendanceCreateDto
    {
        public int EmployeeId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan? CheckIn { get; set; }
        public TimeSpan? CheckOut { get; set; }
        public int LateMinutes { get; set; }
        public bool IsAbsent { get; set; }
    }

    public class AttendanceUpdateDto
    {
        public int EmployeeId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan? CheckIn { get; set; }
        public TimeSpan? CheckOut { get; set; }
        public int LateMinutes { get; set; }
        public bool IsAbsent { get; set; }
    }

    public class AttendanceResponseDto
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } = null!;
        public DateTime Date { get; set; }
        public TimeSpan? CheckIn { get; set; }
        public TimeSpan? CheckOut { get; set; }
        public int LateMinutes { get; set; }
        public bool IsAbsent { get; set; }
    }
}

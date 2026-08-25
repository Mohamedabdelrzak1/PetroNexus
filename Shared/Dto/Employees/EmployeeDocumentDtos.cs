using System;

namespace Shared.Dto.Employees
{
    public class EmployeeDocumentCreateDto
    {
        public int EmployeeId { get; set; }
        public string DocumentName { get; set; } = null!;
        public string FilePath { get; set; } = null!;
        public DateTime? ExpiryDate { get; set; }
    }

    public class EmployeeDocumentUpdateDto
    {
        public int EmployeeId { get; set; }
        public string DocumentName { get; set; } = null!;
        public string FilePath { get; set; } = null!;
        public DateTime? ExpiryDate { get; set; }
    }

    public class EmployeeDocumentResponseDto
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string DocumentName { get; set; } = null!;
        public string FilePath { get; set; } = null!;
        public DateTime? ExpiryDate { get; set; }
    }
}

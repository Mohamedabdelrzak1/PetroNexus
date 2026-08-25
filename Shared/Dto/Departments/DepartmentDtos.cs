using System;
using System.Collections.Generic;

namespace Shared.Dto.Departments
{
    public class DepartmentCreateDto
    {
        public string Name { get; set; } = null!;
    }

    public class DepartmentUpdateDto
    {
        public string Name { get; set; } = null!;
    }

    public class DepartmentResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }

    public class DepartmentDetailsDto : DepartmentResponseDto
    {
        public List<Shared.Dto.Employees.EmployeeResponseDto> Employees { get; set; } = new();
    }
}

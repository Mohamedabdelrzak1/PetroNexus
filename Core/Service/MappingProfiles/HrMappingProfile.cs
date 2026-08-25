using AutoMapper;
using Domain.Models;
using Shared.Dto.Attendances;
using Shared.Dto.Departments;
using Shared.Dto.Employees;
using Shared.Dto.LeaveRequests;
using Shared.Dto.Salaries;

namespace Service.MappingProfiles
{
    public class HrMappingProfile : Profile
    {
        public HrMappingProfile()
        {
            // ==========================================
            // HR
            // ==========================================
            CreateMap<DepartmentCreateDto, Department>();
            CreateMap<DepartmentUpdateDto, Department>();
            CreateMap<Department, DepartmentResponseDto>();
            CreateMap<Department, DepartmentDetailsDto>()
                .ForMember(dest => dest.Employees, opt => opt.MapFrom(src => src.Employees));

            CreateMap<EmployeeCreateDto, Employee>();
            CreateMap<EmployeeUpdateDto, Employee>();
            CreateMap<Employee, EmployeeResponseDto>()
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department != null ? src.Department.Name : string.Empty));
            CreateMap<Employee, EmployeeDetailsDto>()
                .ForMember(dest => dest.Salaries, opt => opt.MapFrom(src => src.Salaries))
                .ForMember(dest => dest.Attendances, opt => opt.MapFrom(src => src.Attendances))
                .ForMember(dest => dest.LeaveRequests, opt => opt.MapFrom(src => src.LeaveRequests))
                .ForMember(dest => dest.Documents, opt => opt.MapFrom(src => src.Documents));

            CreateMap<EmployeeDocumentCreateDto, EmployeeDocument>();
            CreateMap<EmployeeDocumentUpdateDto, EmployeeDocument>();
            CreateMap<EmployeeDocument, EmployeeDocumentResponseDto>();

            CreateMap<AttendanceCreateDto, Attendance>();
            CreateMap<AttendanceUpdateDto, Attendance>();
            CreateMap<Attendance, AttendanceResponseDto>()
                .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee != null ? src.Employee.FullName : string.Empty));

            CreateMap<LeaveRequestCreateDto, LeaveRequest>();
            CreateMap<LeaveRequestUpdateDto, LeaveRequest>();
            CreateMap<LeaveRequest, LeaveRequestResponseDto>()
                .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee != null ? src.Employee.FullName : string.Empty));

            CreateMap<SalaryCreateDto, Salary>();
            CreateMap<SalaryUpdateDto, Salary>();
            CreateMap<Salary, SalaryResponseDto>()
                .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee != null ? src.Employee.FullName : string.Empty));
        }
    }
}
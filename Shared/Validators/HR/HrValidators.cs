using FluentValidation;
using Shared.Dto.Attendances;
using Shared.Dto.Departments;
using Shared.Dto.Employees;
using Shared.Dto.LeaveRequests;
using Shared.Dto.Salaries;

namespace Shared.Validators.HR
{
    public class CreateDepartmentDtoValidator : AbstractValidator<DepartmentCreateDto>
    {
        public CreateDepartmentDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        }
    }

    public class UpdateDepartmentDtoValidator : AbstractValidator<DepartmentUpdateDto>
    {
        public UpdateDepartmentDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        }
    }

    public class CreateEmployeeDtoValidator : AbstractValidator<EmployeeCreateDto>
    {
        public CreateEmployeeDtoValidator()
        {
            RuleFor(x => x.FullName).NotEmpty().MaximumLength(100);
            RuleFor(x => x.DepartmentId).GreaterThan(0);
        }
    }

    public class UpdateEmployeeDtoValidator : AbstractValidator<EmployeeUpdateDto>
    {
        public UpdateEmployeeDtoValidator()
        {
            RuleFor(x => x.FullName).NotEmpty().MaximumLength(100);
            RuleFor(x => x.DepartmentId).GreaterThan(0);
        }
    }

    public class CreateEmployeeDocumentDtoValidator : AbstractValidator<EmployeeDocumentCreateDto>
    {
        public CreateEmployeeDocumentDtoValidator()
        {
            RuleFor(x => x.DocumentName).NotEmpty().MaximumLength(200);
            RuleFor(x => x.FilePath).NotEmpty().MaximumLength(500);
        }
    }

    public class UpdateEmployeeDocumentDtoValidator : AbstractValidator<EmployeeDocumentUpdateDto>
    {
        public UpdateEmployeeDocumentDtoValidator()
        {
            RuleFor(x => x.DocumentName).NotEmpty().MaximumLength(200);
            RuleFor(x => x.FilePath).NotEmpty().MaximumLength(500);
        }
    }

    public class CreateAttendanceDtoValidator : AbstractValidator<AttendanceCreateDto>
    {
        public CreateAttendanceDtoValidator()
        {
            RuleFor(x => x.EmployeeId).GreaterThan(0);
            RuleFor(x => x.CheckIn).NotEmpty();
            RuleFor(x => x.LateMinutes).GreaterThanOrEqualTo(0);
            RuleFor(x => x.CheckOut).Must((dto, checkOut) => checkOut == null || checkOut > dto.CheckIn)
                .WithMessage("CheckOut must be after CheckIn.");
        }
    }

    public class UpdateAttendanceDtoValidator : AbstractValidator<AttendanceUpdateDto>
    {
        public UpdateAttendanceDtoValidator()
        {
            RuleFor(x => x.EmployeeId).GreaterThan(0);
            RuleFor(x => x.CheckIn).NotEmpty();
            RuleFor(x => x.LateMinutes).GreaterThanOrEqualTo(0);
            RuleFor(x => x.CheckOut).Must((dto, checkOut) => checkOut == null || checkOut > dto.CheckIn)
                .WithMessage("CheckOut must be after CheckIn.");
        }
    }

    public class CreateLeaveRequestDtoValidator : AbstractValidator<LeaveRequestCreateDto>
    {
        public CreateLeaveRequestDtoValidator()
        {
            RuleFor(x => x.EmployeeId).GreaterThan(0);
            RuleFor(x => x.Type).IsInEnum();
            RuleFor(x => x.StartDate).NotEmpty();
            RuleFor(x => x.EndDate).GreaterThanOrEqualTo(x => x.StartDate)
                .WithMessage("EndDate must be after or equal to StartDate.");
        }
    }

    public class UpdateLeaveRequestDtoValidator : AbstractValidator<LeaveRequestUpdateDto>
    {
        public UpdateLeaveRequestDtoValidator()
        {
            RuleFor(x => x.EmployeeId).GreaterThan(0);
            RuleFor(x => x.Type).IsInEnum();
            RuleFor(x => x.StartDate).NotEmpty();
            RuleFor(x => x.EndDate).GreaterThanOrEqualTo(x => x.StartDate)
                .WithMessage("EndDate must be after or equal to StartDate.");
        }
    }

    public class CreateSalaryDtoValidator : AbstractValidator<SalaryCreateDto>
    {
        public CreateSalaryDtoValidator()
        {
            RuleFor(x => x.EmployeeId).GreaterThan(0);
            RuleFor(x => x.BasicSalary).GreaterThan(0);
            RuleFor(x => x.Bonus).GreaterThanOrEqualTo(0);
            RuleFor(x => x.OvertimeAmount).GreaterThanOrEqualTo(0);
            RuleFor(x => x.LateDeduction).GreaterThanOrEqualTo(0);
            RuleFor(x => x.AbsenceDeduction).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Month).InclusiveBetween(1, 12);
            RuleFor(x => x.Year).InclusiveBetween(2020, 2100);
        }
    }

    public class UpdateSalaryDtoValidator : AbstractValidator<SalaryUpdateDto>
    {
        public UpdateSalaryDtoValidator()
        {
            RuleFor(x => x.EmployeeId).GreaterThan(0);
            RuleFor(x => x.BasicSalary).GreaterThan(0);
            RuleFor(x => x.Bonus).GreaterThanOrEqualTo(0);
            RuleFor(x => x.OvertimeAmount).GreaterThanOrEqualTo(0);
            RuleFor(x => x.LateDeduction).GreaterThanOrEqualTo(0);
            RuleFor(x => x.AbsenceDeduction).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Month).InclusiveBetween(1, 12);
            RuleFor(x => x.Year).InclusiveBetween(2020, 2100);
        }
    }
}
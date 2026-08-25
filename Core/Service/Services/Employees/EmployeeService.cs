using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using FluentValidation;
using ServiceAbstraction.IEmployees;
using Shared.Dto.Employees;

namespace Service.Services.Employees
{
    public class EmployeeService : BaseService<Employee, int, EmployeeResponseDto, EmployeeCreateDto, EmployeeUpdateDto>, IEmployeeService
    {
        public EmployeeService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<EmployeeCreateDto> createValidator, IValidator<EmployeeUpdateDto> updateValidator)
            : base(unitOfWork, mapper, createValidator, updateValidator)
        {
        }

        public async Task TransferDepartmentAsync(int id, int newDepartmentId, CancellationToken cancellationToken = default)
        {
            var employee = await Repository.GetByIdAsync(id, cancellationToken);
            if (employee == null) throw new KeyNotFoundException($"Employee #{id} was not found.");
            employee.DepartmentId = newDepartmentId;
            Repository.Update(employee);
            await UnitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task TerminateAsync(int id, CancellationToken cancellationToken = default)
        {
            var employee = await Repository.GetByIdAsync(id, cancellationToken);
            if (employee == null) throw new KeyNotFoundException($"Employee #{id} was not found.");
            employee.IsActive = false;
            Repository.Update(employee);
            await UnitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task ActivateAsync(int id, CancellationToken cancellationToken = default)
        {
            var employee = await Repository.GetByIdAsync(id, cancellationToken);
            if (employee == null) throw new KeyNotFoundException($"Employee #{id} was not found.");
            employee.IsActive = true;
            Repository.Update(employee);
            await UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}

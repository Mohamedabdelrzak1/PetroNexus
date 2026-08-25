using System.Threading;
using System.Threading.Tasks;
using Shared.Dto.Employees;

namespace ServiceAbstraction.IEmployees
{
    public interface IEmployeeService : IBaseService<int, EmployeeResponseDto, EmployeeCreateDto, EmployeeUpdateDto>
    {
        Task TransferDepartmentAsync(int id, int newDepartmentId, CancellationToken cancellationToken = default);
        Task TerminateAsync(int id, CancellationToken cancellationToken = default);
        Task ActivateAsync(int id, CancellationToken cancellationToken = default);
    }
}

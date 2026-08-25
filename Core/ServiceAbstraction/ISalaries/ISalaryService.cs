using System.Threading;
using System.Threading.Tasks;
using Shared.Dto.Salaries;

namespace ServiceAbstraction.ISalaries
{
    public interface ISalaryService : IBaseService<int, SalaryResponseDto, SalaryCreateDto, SalaryUpdateDto>
    {
        Task<SalaryResponseDto> GenerateSalaryAsync(int employeeId, int month, int year, CancellationToken cancellationToken = default);
        Task ApproveAsync(int id, CancellationToken cancellationToken = default);
        Task MarkAsPaidAsync(int id, CancellationToken cancellationToken = default);
    }
}

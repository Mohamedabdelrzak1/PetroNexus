using System.Threading;
using System.Threading.Tasks;
using Shared.Dto.Employees;

namespace ServiceAbstraction.IEmployees
{
    public interface IEmployeeDocumentService : IBaseService<int, EmployeeDocumentResponseDto, EmployeeDocumentCreateDto, EmployeeDocumentUpdateDto>
    {
    }
}

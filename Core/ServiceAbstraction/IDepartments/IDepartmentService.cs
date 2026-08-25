using System.Threading;
using System.Threading.Tasks;
using Shared.Dto.Departments;

namespace ServiceAbstraction.IDepartments
{
    public interface IDepartmentService : IBaseService<int, DepartmentResponseDto, DepartmentCreateDto, DepartmentUpdateDto>
    {
    }
}

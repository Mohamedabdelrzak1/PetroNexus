using System.Threading;
using System.Threading.Tasks;
using Shared.Dto.EngineeringProjects;

namespace ServiceAbstraction.IEngineeringProjects
{
    public interface IEngineeringProjectService : IBaseService<int, EngineeringProjectResponseDto, EngineeringProjectCreateDto, EngineeringProjectUpdateDto>
    {
    }
}

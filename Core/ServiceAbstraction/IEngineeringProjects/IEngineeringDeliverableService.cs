using System.Threading;
using System.Threading.Tasks;
using Shared.Dto.EngineeringProjects;

namespace ServiceAbstraction.IEngineeringProjects
{
    public interface IEngineeringDeliverableService : IBaseService<int, EngineeringDeliverableResponseDto, EngineeringDeliverableCreateDto, EngineeringDeliverableUpdateDto>
    {
    }
}

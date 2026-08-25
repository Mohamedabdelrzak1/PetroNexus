using System.Threading;
using System.Threading.Tasks;
using Shared.Dto.CostCenters;

namespace ServiceAbstraction.ICostCenters
{
    public interface ICostCenterService : IBaseService<int, CostCenterResponseDto, CostCenterCreateDto, CostCenterUpdateDto>
    {
    }
}

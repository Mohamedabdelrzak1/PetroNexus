using System.Threading;
using System.Threading.Tasks;
using Shared.Dto.FabricationOrders;

namespace ServiceAbstraction.IFabricationOrders
{
    public interface IFabricationOrderService : IBaseService<int, FabricationOrderResponseDto, FabricationOrderCreateDto, FabricationOrderUpdateDto>
    {
    }
}

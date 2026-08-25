using System.Threading;
using System.Threading.Tasks;
using Shared.Dto.PurchaseOrders;

namespace ServiceAbstraction.IPurchaseOrders
{
    public interface IPurchaseOrderService : IBaseService<int, PurchaseOrderResponseDto, PurchaseOrderCreateDto, PurchaseOrderUpdateDto>
    {
    }
}

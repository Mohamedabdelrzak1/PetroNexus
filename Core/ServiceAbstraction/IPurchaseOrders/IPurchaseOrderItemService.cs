using System.Threading;
using System.Threading.Tasks;
using Shared.Dto.PurchaseOrders;

namespace ServiceAbstraction.IPurchaseOrders
{
    public interface IPurchaseOrderItemService : IBaseService<int, PurchaseOrderItemResponseDto, PurchaseOrderItemCreateDto, PurchaseOrderItemUpdateDto>
    {
    }
}

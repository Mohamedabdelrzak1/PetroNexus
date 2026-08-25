using System.Threading;
using System.Threading.Tasks;

namespace ServiceAbstraction.IInventory
{
    public interface IInventoryService
    {
        Task ReceiveItemsAsync(int orderId, CancellationToken cancellationToken = default);
        Task IssueItemsAsync(int orderId, CancellationToken cancellationToken = default);
        Task TransferItemsAsync(int orderId, CancellationToken cancellationToken = default);
    }
}

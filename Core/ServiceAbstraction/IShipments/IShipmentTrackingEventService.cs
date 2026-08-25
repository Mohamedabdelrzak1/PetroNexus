using System.Threading;
using System.Threading.Tasks;
using Shared.Dto.Shipments;

namespace ServiceAbstraction.IShipments
{
    public interface IShipmentTrackingEventService : IBaseService<int, ShipmentTrackingEventResponseDto, ShipmentTrackingEventCreateDto, ShipmentTrackingEventUpdateDto>
    {
    }
}

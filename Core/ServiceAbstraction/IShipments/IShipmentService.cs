using System.Threading;
using System.Threading.Tasks;
using Shared.Dto.Shipments;

namespace ServiceAbstraction.IShipments
{
    public interface IShipmentService : IBaseService<int, ShipmentResponseDto, ShipmentCreateDto, ShipmentUpdateDto>
    {
    }
}

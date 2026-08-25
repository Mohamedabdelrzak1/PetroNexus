using System.Threading;
using System.Threading.Tasks;
using Shared.Dto.VendorRegistrations;

namespace ServiceAbstraction.IVendorRegistrations
{
    public interface IVendorRegistrationService : IBaseService<int, VendorRegistrationResponseDto, VendorRegistrationCreateDto, VendorRegistrationUpdateDto>
    {
    }
}

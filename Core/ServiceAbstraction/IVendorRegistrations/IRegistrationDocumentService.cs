using System.Threading;
using System.Threading.Tasks;
using Shared.Dto.RegistrationDocuments;

namespace ServiceAbstraction.IVendorRegistrations
{
    public interface IRegistrationDocumentService : IBaseService<int, RegistrationDocumentResponseDto, RegistrationDocumentCreateDto, RegistrationDocumentUpdateDto>
    {
    }
}

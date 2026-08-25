using System.Threading;
using System.Threading.Tasks;
using Shared.Dto.Payments;

namespace ServiceAbstraction.IPayments
{
    public interface IPaymentService : IBaseService<int, PaymentResponseDto, PaymentCreateDto, PaymentUpdateDto>
    {
    }
}

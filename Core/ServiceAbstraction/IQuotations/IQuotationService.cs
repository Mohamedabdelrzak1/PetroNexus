using System.Threading;
using System.Threading.Tasks;
using Shared.Dto.Quotations;

namespace ServiceAbstraction.IQuotations
{
    public interface IQuotationService : IBaseService<int, QuotationResponseDto, QuotationCreateDto, QuotationUpdateDto>
    {
    }
}

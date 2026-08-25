using System.Threading;
using System.Threading.Tasks;
using Shared.Dto.Quotations;

namespace ServiceAbstraction.IQuotations
{
    public interface IQuotationItemService : IBaseService<int, QuotationItemResponseDto, QuotationItemCreateDto, QuotationItemUpdateDto>
    {
    }
}

using System.Threading;
using System.Threading.Tasks;
using Shared.Dto.Invoices;

namespace ServiceAbstraction.IInvoices
{
    public interface IInvoiceItemService : IBaseService<int, InvoiceItemResponseDto, InvoiceItemCreateDto, InvoiceItemUpdateDto>
    {
    }
}

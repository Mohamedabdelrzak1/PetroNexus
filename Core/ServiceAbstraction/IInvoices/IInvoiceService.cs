using System.Threading;
using System.Threading.Tasks;
using Shared.Dto.Invoices;

namespace ServiceAbstraction.IInvoices
{
    public interface IInvoiceService : IBaseService<int, InvoiceResponseDto, InvoiceCreateDto, InvoiceUpdateDto>
    {
        Task<InvoiceResponseDto> GenerateInvoiceAsync(int tenderId, CancellationToken cancellationToken = default);
        Task ApproveAsync(int id, CancellationToken cancellationToken = default);
        Task CancelAsync(int id, CancellationToken cancellationToken = default);
        Task RecordPaymentAsync(int invoiceId, decimal amount, string method, string referenceNumber, CancellationToken cancellationToken = default);
    }
}

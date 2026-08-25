using System.Threading;
using System.Threading.Tasks;
using Domain.Models;

namespace ServiceAbstraction.IJournalPosting
{
    /// <summary>
    /// Centralized journal posting engine.
    /// Generates balanced JournalEntry + JournalEntryLine[] automatically
    /// for every financial event in the system.
    /// </summary>
    public interface IJournalPostingService
    {
        /// <summary>Posts a journal entry for an Invoice issuance.</summary>
        Task PostInvoiceAsync(Invoice invoice, CancellationToken cancellationToken = default);

        /// <summary>Posts a journal entry for a received Payment.</summary>
        Task PostPaymentAsync(Payment payment, Invoice invoice, CancellationToken cancellationToken = default);

        /// <summary>Posts a journal entry for an approved Salary.</summary>
        Task PostSalaryAsync(Salary salary, CancellationToken cancellationToken = default);

        /// <summary>Posts a journal entry for a PurchaseOrder issuance.</summary>
        Task PostPurchaseOrderAsync(PurchaseOrder purchaseOrder, CancellationToken cancellationToken = default);

        /// <summary>Posts a journal entry for Shipment costs (shipping/customs).</summary>
        Task PostShipmentAsync(Shipment shipment, CancellationToken cancellationToken = default);

        /// <summary>Posts a journal entry for FabricationOrder costs.</summary>
        Task PostFabricationOrderAsync(FabricationOrder fabricationOrder, CancellationToken cancellationToken = default);

        /// <summary>Posts a journal entry for LetterOfCredit opening.</summary>
        Task PostLetterOfCreditAsync(LetterOfCredit letterOfCredit, CancellationToken cancellationToken = default);

        /// <summary>Posts a journal entry for Commission receipt.</summary>
        Task PostCommissionAsync(Commission commission, CancellationToken cancellationToken = default);
    }
}
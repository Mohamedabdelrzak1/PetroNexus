using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Shared.Dto.Dashboard;

namespace ServiceAbstraction.IDashboard
{
    /// <summary>
    /// Dashboard & Analytics service — the competitive edge of PetroNexus.
    /// Aggregates data from Tenders, Shipments, Principals, Invoices, and Payments
    /// to power the executive dashboards.
    /// </summary>
    public interface IDashboardService
    {
        /// <summary>Overall tender win rate, broken down by client and sector.</summary>
        Task<TenderWinRateDto> GetTenderWinRateAsync(CancellationToken cancellationToken = default);

        /// <summary>Comprehensive expediting board — all active shipments with risk flags.</summary>
        Task<ExpeditingOverviewDto> GetExpeditingOverviewAsync(CancellationToken cancellationToken = default);

        /// <summary>Per-Principal profitability: PO value, commissions, performance, NCRs.</summary>
        Task<PrincipalProfitabilityDto> GetPrincipalProfitabilityAsync(CancellationToken cancellationToken = default);

        /// <summary>Revenue forecast based on in-progress tenders × probability factor.</summary>
        Task<RevenueForecastDto> GetRevenueForecastAsync(CancellationToken cancellationToken = default);

        /// <summary>Financial summary: invoiced, paid, outstanding by currency.</summary>
        Task<FinancialSummaryDto> GetFinancialSummaryAsync(CancellationToken cancellationToken = default);

        /// <summary>Monthly invoiced vs collected for the last 7 months (Bar Chart).</summary>
        Task<List<RevenueCollectionMonthlyDto>> GetRevenueCollectionMonthlyAsync(CancellationToken cancellationToken = default);

        /// <summary>Active tender pipeline distribution (Pie/Donut Chart).</summary>
        Task<List<TenderPipelineDto>> GetTenderPipelineAsync(CancellationToken cancellationToken = default);
    }
}
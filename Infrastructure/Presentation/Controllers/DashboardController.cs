using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.Common;
using Shared.Dto.Dashboard;

namespace Presentation.Controllers
{
    /// <summary>
    /// Dashboard & Analytics endpoints — the competitive edge of PetroNexus.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class DashboardController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public DashboardController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        /// <summary>Overall tender win rate, broken down by client and sector.</summary>
        [HttpGet("tender-win-rate")]
        public async Task<ActionResult<ApiResponse<TenderWinRateDto>>> GetTenderWinRate(CancellationToken cancellationToken = default)
        {
            var result = await _serviceManager.DashboardService.GetTenderWinRateAsync(cancellationToken);
            return this.OkResponse(result);
        }

        /// <summary>Comprehensive expediting board — all active shipments with risk flags.</summary>
        [HttpGet("expediting-overview")]
        public async Task<ActionResult<ApiResponse<ExpeditingOverviewDto>>> GetExpeditingOverview(CancellationToken cancellationToken = default)
        {
            var result = await _serviceManager.DashboardService.GetExpeditingOverviewAsync(cancellationToken);
            return this.OkResponse(result);
        }

        /// <summary>Per-Principal profitability: PO value, commissions, performance, NCRs.</summary>
        [HttpGet("principal-profitability")]
        public async Task<ActionResult<ApiResponse<PrincipalProfitabilityDto>>> GetPrincipalProfitability(CancellationToken cancellationToken = default)
        {
            var result = await _serviceManager.DashboardService.GetPrincipalProfitabilityAsync(cancellationToken);
            return this.OkResponse(result);
        }

        /// <summary>Revenue forecast based on in-progress tenders × probability factor.</summary>
        [HttpGet("revenue-forecast")]
        public async Task<ActionResult<ApiResponse<RevenueForecastDto>>> GetRevenueForecast(CancellationToken cancellationToken = default)
        {
            var result = await _serviceManager.DashboardService.GetRevenueForecastAsync(cancellationToken);
            return this.OkResponse(result);
        }

        /// <summary>Financial summary: invoiced, paid, outstanding by currency.</summary>
        [HttpGet("financial-summary")]
        public async Task<ActionResult<ApiResponse<FinancialSummaryDto>>> GetFinancialSummary(CancellationToken cancellationToken = default)
        {
            var result = await _serviceManager.DashboardService.GetFinancialSummaryAsync(cancellationToken);
            return this.OkResponse(result);
        }

        /// <summary>Monthly invoiced vs collected for the last 7 months (Bar Chart).</summary>
        [HttpGet("revenue-collection-monthly")]
        public async Task<ActionResult<ApiResponse<List<RevenueCollectionMonthlyDto>>>> GetRevenueCollectionMonthly(CancellationToken cancellationToken = default)
        {
            var result = await _serviceManager.DashboardService.GetRevenueCollectionMonthlyAsync(cancellationToken);
            return this.OkResponse(result);
        }

        /// <summary>Active tender pipeline distribution (Pie/Donut Chart).</summary>
        [HttpGet("tender-pipeline")]
        public async Task<ActionResult<ApiResponse<List<TenderPipelineDto>>>> GetTenderPipeline(CancellationToken cancellationToken = default)
        {
            var result = await _serviceManager.DashboardService.GetTenderPipelineAsync(cancellationToken);
            return this.OkResponse(result);
        }
    }
}
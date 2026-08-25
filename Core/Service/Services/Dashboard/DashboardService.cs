using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Enums;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using ServiceAbstraction.IDashboard;
using Shared.Dto.Dashboard;

namespace Service.Services.Dashboard
{
    /// <summary>
    /// Dashboard & Analytics service implementation.
    /// Aggregates data from Tenders, Shipments, Principals, Invoices, and Payments.
    /// </summary>
    public class DashboardService : IDashboardService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DashboardService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        // =========================================================
        // 1) Tender Win Rate
        // =========================================================
        public async Task<TenderWinRateDto> GetTenderWinRateAsync(CancellationToken cancellationToken = default)
        {
            var tenders = await _unitOfWork.Repository<Tender, int>()
                .GetAllQueryable(false)
                .Include(t => t.Client)
                .ToListAsync(cancellationToken);

            var total = tenders.Count;
            var won = tenders.Count(t => t.Status == TenderStatus.Won);
            var lost = tenders.Count(t => t.Status == TenderStatus.Lost);
            var inProgress = tenders.Count(t => t.Status == TenderStatus.UnderPreparation || t.Status == TenderStatus.Submitted);
            var winRate = total > 0 ? Math.Round((decimal)won / total * 100, 2) : 0;

            // New leads this week (last 7 days)
            var weekAgo = DateTime.UtcNow.AddDays(-7);
            var newLeadsThisWeek = await _unitOfWork.Repository<TenderLead, int>()
                .GetAllQueryable(false)
                .CountAsync(l => l.DiscoveredAt >= weekAgo, cancellationToken);

            // By client
            var byClient = tenders
                .GroupBy(t => t.Client?.Name ?? "Unknown")
                .Select(g => new ClientWinRateDto
                {
                    ClientName = g.Key,
                    Total = g.Count(),
                    Won = g.Count(t => t.Status == TenderStatus.Won),
                    WinRate = g.Count() > 0 ? Math.Round((decimal)g.Count(t => t.Status == TenderStatus.Won) / g.Count() * 100, 2) : 0
                })
                .OrderByDescending(x => x.Total)
                .ToList();

            // By sector
            var bySector = tenders
                .GroupBy(t => t.Client?.Sector ?? "Unknown")
                .Select(g => new SectorWinRateDto
                {
                    Sector = g.Key,
                    Total = g.Count(),
                    Won = g.Count(t => t.Status == TenderStatus.Won),
                    WinRate = g.Count() > 0 ? Math.Round((decimal)g.Count(t => t.Status == TenderStatus.Won) / g.Count() * 100, 2) : 0
                })
                .OrderByDescending(x => x.Total)
                .ToList();

            return new TenderWinRateDto
            {
                TotalTenders = total,
                Won = won,
                Lost = lost,
                InProgress = inProgress,
                WinRatePercentage = winRate,
                NewLeadsThisWeek = newLeadsThisWeek,
                ByClient = byClient,
                BySector = bySector
            };
        }

        // =========================================================
        // 2) Expediting Overview
        // =========================================================
        public async Task<ExpeditingOverviewDto> GetExpeditingOverviewAsync(CancellationToken cancellationToken = default)
        {
            // Active shipments = not Delivered
            var activeShipments = await _unitOfWork.Repository<Shipment, int>()
                .GetAllQueryable(false)
                .Include(s => s.PurchaseOrder)
                .Include(s => s.LiquidatedDamages)
                .Where(s => s.Status != ShipmentStatus.Delivered)
                .ToListAsync(cancellationToken);

            // Count by status
            var shipmentsByStatus = activeShipments
                .GroupBy(s => s.Status)
                .ToDictionary(g => g.Key.ToString(), g => g.Count());

            // Build active shipment DTOs
            var activeDtos = activeShipments
                .Select(s => new ExpeditingShipmentDto
                {
                    Id = s.Id,
                    PurchaseOrderId = s.PurchaseOrderId,
                    PoNumber = s.PurchaseOrder?.PoNumber ?? $"PO-{s.PurchaseOrderId}",
                    TrackingNumber = s.TrackingNumber,
                    Status = s.Status,
                    PlannedDeliveryDate = s.PlannedDeliveryDate,
                    IsAtRiskOfDelay = s.IsAtRiskOfDelay,
                    LiquidatedDamages = s.LiquidatedDamages.Select(ld => new LiquidatedDamageSummaryDto
                    {
                        Id = ld.Id,
                        DaysDelayed = ld.DaysDelayed,
                        CalculatedAmount = ld.CalculatedAmount,
                        Currency = ld.Currency,
                        Status = ld.Status
                    }).ToList()
                })
                .OrderBy(s => s.IsAtRiskOfDelay ? 0 : 1)   // At-risk first
                .ThenBy(s => s.PlannedDeliveryDate ?? DateTime.MaxValue) // Soonest deadline next
                .ToList();

            // At-risk shipments (IsAtRiskOfDelay = true OR deadline within 14 days)
            var atRisk = activeDtos
                .Where(s => s.IsAtRiskOfDelay || (s.PlannedDeliveryDate.HasValue && s.PlannedDeliveryDate.Value <= DateTime.UtcNow.AddDays(14)))
                .Select(s => new AtRiskShipmentDto
                {
                    Id = s.Id,
                    PurchaseOrderId = s.PurchaseOrderId,
                    PoNumber = s.PoNumber,
                    TrackingNumber = s.TrackingNumber,
                    Status = s.Status,
                    PlannedDeliveryDate = s.PlannedDeliveryDate,
                    IsAtRiskOfDelay = s.IsAtRiskOfDelay,
                    DaysUntilDeadline = s.PlannedDeliveryDate.HasValue
                        ? (int?)Math.Max(0, (s.PlannedDeliveryDate.Value - DateTime.UtcNow).Days)
                        : null,
                    LiquidatedDamages = s.LiquidatedDamages
                })
                .OrderBy(s => s.DaysUntilDeadline ?? int.MaxValue)
                .ToList();

            return new ExpeditingOverviewDto
            {
                ShipmentsByStatus = shipmentsByStatus,
                AtRiskShipments = atRisk,
                ActiveShipments = activeDtos
            };
        }

        // =========================================================
        // 3) Principal Profitability
        // =========================================================
        public async Task<PrincipalProfitabilityDto> GetPrincipalProfitabilityAsync(CancellationToken cancellationToken = default)
        {
            var principals = await _unitOfWork.Repository<Principal, int>()
                .GetAllQueryable(false)
                .Include(p => p.Commissions)
                .Include(p => p.PerformanceReviews)
                .ToListAsync(cancellationToken);

            // Load PurchaseOrders with items separately (Principal has no PO navigation collection)
            var purchaseOrders = await _unitOfWork.Repository<PurchaseOrder, int>()
                .GetAllQueryable(false)
                .Include(po => po.Items)
                .ToListAsync(cancellationToken);

            // Get all NCRs linked to principals via FabricationOrder → PurchaseOrder → Principal
            var ncrCounts = new Dictionary<int, int>();
            var fabricationOrders = await _unitOfWork.Repository<FabricationOrder, int>()
                .GetAllQueryable(false)
                .Include(f => f.Inspections)
                    .ThenInclude(i => i.NonConformanceReports)
                .ToListAsync(cancellationToken);

            foreach (var po in purchaseOrders)
            {
                var relatedFabs = fabricationOrders.Where(f => f.PurchaseOrderId == po.Id).ToList();
                var ncrCount = relatedFabs
                    .SelectMany(f => f.Inspections)
                    .SelectMany(i => i.NonConformanceReports)
                    .Count();

                if (ncrCount > 0)
                {
                    if (!ncrCounts.ContainsKey(po.PrincipalId))
                        ncrCounts[po.PrincipalId] = 0;
                    ncrCounts[po.PrincipalId] += ncrCount;
                }
            }

            var items = principals.Select(p =>
            {
                var principalPOs = purchaseOrders.Where(po => po.PrincipalId == p.Id).ToList();
                var totalPoValue = principalPOs
                    .SelectMany(po => po.Items ?? new List<PurchaseOrderItem>())
                    .Sum(i => i.UnitCost * i.Quantity);

                var totalCommissions = p.Commissions?.Sum(c => c.Amount) ?? 0m;

                var avgScore = p.PerformanceReviews != null && p.PerformanceReviews.Any()
                    ? p.PerformanceReviews.Average(r => r.AverageScore)
                    : p.PerformanceScore;

                return new PrincipalProfitabilityItemDto
                {
                    PrincipalId = p.Id,
                    PrincipalName = p.Name,
                    TotalPurchaseOrderValue = totalPoValue,
                    TotalCommissions = totalCommissions,
                    AveragePerformanceScore = Math.Round(avgScore, 2),
                    NonConformanceCount = ncrCounts.ContainsKey(p.Id) ? ncrCounts[p.Id] : 0
                };
            })
            .OrderByDescending(x => x.TotalPurchaseOrderValue)
            .ToList();

            return new PrincipalProfitabilityDto { Items = items };
        }

        // =========================================================
        // 4) Revenue Forecast
        // =========================================================
        public async Task<RevenueForecastDto> GetRevenueForecastAsync(CancellationToken cancellationToken = default)
        {
            // Probability factors (documented as estimates, not exact):
            //   Submitted = 50% chance of winning
            //   UnderPreparation = 20% chance of winning
            const decimal submittedFactor = 0.50m;
            const decimal underPrepFactor = 0.20m;

            var activeTenders = await _unitOfWork.Repository<Tender, int>()
                .GetAllQueryable(false)
                .Where(t => t.Status == TenderStatus.Submitted || t.Status == TenderStatus.UnderPreparation)
                .ToListAsync(cancellationToken);

            var items = activeTenders.Select(t =>
            {
                var factor = t.Status == TenderStatus.Submitted ? submittedFactor : underPrepFactor;
                var estimatedValue = t.EstimatedValue ?? 0m;
                return new RevenueForecastItemDto
                {
                    TenderId = t.Id,
                    Title = t.Title,
                    Status = t.Status,
                    EstimatedValue = estimatedValue,
                    ProbabilityFactor = factor,
                    ExpectedValue = estimatedValue * factor
                };
            }).ToList();

            var submittedValue = items.Where(i => i.Status == TenderStatus.Submitted).Sum(i => i.EstimatedValue);
            var underPrepValue = items.Where(i => i.Status == TenderStatus.UnderPreparation).Sum(i => i.EstimatedValue);

            return new RevenueForecastDto
            {
                TotalForecast = items.Sum(i => i.ExpectedValue),
                SubmittedValue = submittedValue,
                UnderPreparationValue = underPrepValue,
                Items = items
            };
        }

        // =========================================================
        // 5) Financial Summary
        // =========================================================
        public async Task<FinancialSummaryDto> GetFinancialSummaryAsync(CancellationToken cancellationToken = default)
        {
            var invoices = await _unitOfWork.Repository<Invoice, int>()
                .GetAllQueryable(false)
                .Include(i => i.Items)
                .Include(i => i.Payments)
                .ToListAsync(cancellationToken);

            var totalInvoiced = invoices.Sum(i => i.TotalAmount);
            var totalPaid = invoices.Sum(i => i.TotalPaid);
            var totalOutstanding = totalInvoiced - totalPaid;

            // Balances by currency
            var balances = invoices
                .GroupBy(i => i.Currency)
                .Select(g => new CurrencyBalanceDto
                {
                    Currency = g.Key,
                    Invoiced = g.Sum(i => i.TotalAmount),
                    Paid = g.Sum(i => i.TotalPaid),
                    Outstanding = g.Sum(i => i.TotalAmount) - g.Sum(i => i.TotalPaid)
                })
                .OrderBy(b => b.Currency)
                .ToList();

            return new FinancialSummaryDto
            {
                TotalInvoiced = totalInvoiced,
                TotalPaid = totalPaid,
                TotalOutstanding = totalOutstanding,
                BalancesByCurrency = balances
            };
        }

        // =========================================================
        // 6) Revenue Collection Monthly (Bar Chart)
        // =========================================================
        public async Task<List<RevenueCollectionMonthlyDto>> GetRevenueCollectionMonthlyAsync(CancellationToken cancellationToken = default)
        {
            var now = DateTime.UtcNow;
            var startDate = new DateTime(now.Year, now.Month, 1).AddMonths(-6); // last 7 months including current

            var invoices = await _unitOfWork.Repository<Invoice, int>()
                .GetAllQueryable(false)
                .Include(i => i.Items)
                .Where(i => i.IssueDate >= startDate)
                .ToListAsync(cancellationToken);

            var payments = await _unitOfWork.Repository<Payment, int>()
                .GetAllQueryable(false)
                .Where(p => p.PaymentDate >= startDate)
                .ToListAsync(cancellationToken);

            var monthNames = new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };

            var result = new List<RevenueCollectionMonthlyDto>();
            for (int i = 0; i < 7; i++)
            {
                var monthStart = startDate.AddMonths(i);
                var monthEnd = monthStart.AddMonths(1);

                var invoiced = invoices
                    .Where(inv => inv.IssueDate >= monthStart && inv.IssueDate < monthEnd)
                    .Sum(inv => inv.TotalAmount);

                var collected = payments
                    .Where(p => p.PaymentDate >= monthStart && p.PaymentDate < monthEnd)
                    .Sum(p => p.Amount);

                result.Add(new RevenueCollectionMonthlyDto
                {
                    Month = monthNames[monthStart.Month - 1],
                    Invoiced = invoiced,
                    Collected = collected
                });
            }

            return result;
        }

        // =========================================================
        // 7) Tender Pipeline (Pie/Donut Chart)
        // =========================================================
        public async Task<List<TenderPipelineDto>> GetTenderPipelineAsync(CancellationToken cancellationToken = default)
        {
            var tenders = await _unitOfWork.Repository<Tender, int>()
                .GetAllQueryable(false)
                .ToListAsync(cancellationToken);

            return new List<TenderPipelineDto>
            {
                new TenderPipelineDto { Stage = "Won", Count = tenders.Count(t => t.Status == TenderStatus.Won) },
                new TenderPipelineDto { Stage = "Submitted", Count = tenders.Count(t => t.Status == TenderStatus.Submitted) },
                new TenderPipelineDto { Stage = "UnderPrep", Count = tenders.Count(t => t.Status == TenderStatus.UnderPreparation) },
                new TenderPipelineDto { Stage = "Lost", Count = tenders.Count(t => t.Status == TenderStatus.Lost) }
            };
        }
    }
}
using System;
using System.Collections.Generic;
using Domain.Enums;

namespace Shared.Dto.Dashboard
{
    // =========================================================
    // 1) Tender Win Rate
    // =========================================================
    public class TenderWinRateDto
    {
        public int TotalTenders { get; set; }
        public int Won { get; set; }
        public int Lost { get; set; }
        public int InProgress { get; set; }
        public decimal WinRatePercentage { get; set; }
        public int NewLeadsThisWeek { get; set; }
        public List<ClientWinRateDto> ByClient { get; set; } = new();
        public List<SectorWinRateDto> BySector { get; set; } = new();
    }

    public class ClientWinRateDto
    {
        public string ClientName { get; set; } = null!;
        public int Total { get; set; }
        public int Won { get; set; }
        public decimal WinRate { get; set; }
    }

    public class SectorWinRateDto
    {
        public string Sector { get; set; } = null!;
        public int Total { get; set; }
        public int Won { get; set; }
        public decimal WinRate { get; set; }
    }

    // =========================================================
    // 2) Expediting Overview
    // =========================================================
    public class ExpeditingOverviewDto
    {
        public Dictionary<string, int> ShipmentsByStatus { get; set; } = new();
        public List<AtRiskShipmentDto> AtRiskShipments { get; set; } = new();
        public List<ExpeditingShipmentDto> ActiveShipments { get; set; } = new();
    }

    public class ExpeditingShipmentDto
    {
        public int Id { get; set; }
        public int PurchaseOrderId { get; set; }
        public string PoNumber { get; set; } = null!;
        public string? TrackingNumber { get; set; }
        public ShipmentStatus Status { get; set; }
        public DateTime? PlannedDeliveryDate { get; set; }
        public bool IsAtRiskOfDelay { get; set; }
        public List<LiquidatedDamageSummaryDto> LiquidatedDamages { get; set; } = new();
    }

    public class AtRiskShipmentDto
    {
        public int Id { get; set; }
        public int PurchaseOrderId { get; set; }
        public string PoNumber { get; set; } = null!;
        public string? TrackingNumber { get; set; }
        public ShipmentStatus Status { get; set; }
        public DateTime? PlannedDeliveryDate { get; set; }
        public bool IsAtRiskOfDelay { get; set; }
        public int? DaysUntilDeadline { get; set; }
        public List<LiquidatedDamageSummaryDto> LiquidatedDamages { get; set; } = new();
    }

    public class LiquidatedDamageSummaryDto
    {
        public int Id { get; set; }
        public int DaysDelayed { get; set; }
        public decimal CalculatedAmount { get; set; }
        public CurrencyType Currency { get; set; }
        public LdStatus Status { get; set; }
    }

    // =========================================================
    // 3) Principal Profitability
    // =========================================================
    public class PrincipalProfitabilityDto
    {
        public List<PrincipalProfitabilityItemDto> Items { get; set; } = new();
    }

    public class PrincipalProfitabilityItemDto
    {
        public int PrincipalId { get; set; }
        public string PrincipalName { get; set; } = null!;
        public decimal TotalPurchaseOrderValue { get; set; }
        public decimal TotalCommissions { get; set; }
        public double AveragePerformanceScore { get; set; }
        public int NonConformanceCount { get; set; }
    }

    // =========================================================
    // 4) Revenue Forecast
    // =========================================================
    public class RevenueForecastDto
    {
        public decimal TotalForecast { get; set; }
        public decimal SubmittedValue { get; set; }
        public decimal UnderPreparationValue { get; set; }
        public List<RevenueForecastItemDto> Items { get; set; } = new();
    }

    public class RevenueForecastItemDto
    {
        public int TenderId { get; set; }
        public string Title { get; set; } = null!;
        public TenderStatus Status { get; set; }
        public decimal EstimatedValue { get; set; }
        public decimal ProbabilityFactor { get; set; }
        public decimal ExpectedValue { get; set; }
    }

    // =========================================================
    // 5) Financial Summary
    // =========================================================
    public class FinancialSummaryDto
    {
        public decimal TotalInvoiced { get; set; }
        public decimal TotalPaid { get; set; }
        public decimal TotalOutstanding { get; set; }
        public List<CurrencyBalanceDto> BalancesByCurrency { get; set; } = new();
    }

    public class CurrencyBalanceDto
    {
        public CurrencyType Currency { get; set; }
        public decimal Invoiced { get; set; }
        public decimal Paid { get; set; }
        public decimal Outstanding { get; set; }
    }

    // =========================================================
    // 6) Revenue Collection Monthly (Bar Chart)
    // =========================================================
    public class RevenueCollectionMonthlyDto
    {
        public string Month { get; set; } = null!;
        public decimal Invoiced { get; set; }
        public decimal Collected { get; set; }
    }

    // =========================================================
    // 7) Tender Pipeline (Pie/Donut Chart)
    // =========================================================
    public class TenderPipelineDto
    {
        public string Stage { get; set; } = null!;
        public int Count { get; set; }
    }
}
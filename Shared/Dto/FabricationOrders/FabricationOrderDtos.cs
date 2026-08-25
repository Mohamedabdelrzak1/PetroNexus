using Shared.Dto.NonConformanceReports;
using System;
using System.Collections.Generic;

namespace Shared.Dto.FabricationOrders
{
    public class FabricationOrderCreateDto
    {
        public int PurchaseOrderId { get; set; }
        public string FabricatorName { get; set; } = null!;
        public double? FabricatorRating { get; set; }
        public DateTime PlannedStartDate { get; set; }
        public DateTime PlannedFinishDate { get; set; }
        public DateTime? ActualStartDate { get; set; }
        public DateTime? ActualFinishDate { get; set; }
        public int CompletionPercentage { get; set; }
        public decimal? FabricationCost { get; set; }
        public int? JournalEntryId { get; set; }
    }

    public class FabricationOrderUpdateDto
    {
        public int PurchaseOrderId { get; set; }
        public string FabricatorName { get; set; } = null!;
        public double? FabricatorRating { get; set; }
        public DateTime PlannedStartDate { get; set; }
        public DateTime PlannedFinishDate { get; set; }
        public DateTime? ActualStartDate { get; set; }
        public DateTime? ActualFinishDate { get; set; }
        public int CompletionPercentage { get; set; }
        public decimal? FabricationCost { get; set; }
        public int? JournalEntryId { get; set; }
    }

    public class FabricationOrderResponseDto
    {
        public int Id { get; set; }
        public int PurchaseOrderId { get; set; }
        public string PoNumber { get; set; } = null!;
        public string FabricatorName { get; set; } = null!;
        public double? FabricatorRating { get; set; }
        public DateTime PlannedStartDate { get; set; }
        public DateTime PlannedFinishDate { get; set; }
        public DateTime? ActualStartDate { get; set; }
        public DateTime? ActualFinishDate { get; set; }
        public int CompletionPercentage { get; set; }
        public decimal? FabricationCost { get; set; }
        public int? JournalEntryId { get; set; }
    }

    public class FabricationOrderDetailsDto : FabricationOrderResponseDto
    {
        public List<QualityInspectionResponseDto> Inspections { get; set; } = new();
    }
}

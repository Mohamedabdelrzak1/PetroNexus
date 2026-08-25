using System;
using System.Collections.Generic;
using Domain.Enums;

namespace Shared.Dto.NonConformanceReports
{
    public class QualityInspectionCreateDto
    {
        public int FabricationOrderId { get; set; }
        public DateTime InspectionDate { get; set; }
        public string InspectorName { get; set; } = null!;
        public InspectionResult Result { get; set; }
        public string? Notes { get; set; }
        public string? PhotoPath { get; set; }
    }

    public class QualityInspectionUpdateDto
    {
        public int FabricationOrderId { get; set; }
        public DateTime InspectionDate { get; set; }
        public string InspectorName { get; set; } = null!;
        public InspectionResult Result { get; set; }
        public string? Notes { get; set; }
        public string? PhotoPath { get; set; }
    }

    public class QualityInspectionResponseDto
    {
        public int Id { get; set; }
        public int FabricationOrderId { get; set; }
        public DateTime InspectionDate { get; set; }
        public string InspectorName { get; set; } = null!;
        public InspectionResult Result { get; set; }
        public string? Notes { get; set; }
        public string? PhotoPath { get; set; }
    }

    public class QualityInspectionDetailsDto : QualityInspectionResponseDto
    {
        public List<NonConformanceReportResponseDto> NonConformanceReports { get; set; } = new();
    }
}

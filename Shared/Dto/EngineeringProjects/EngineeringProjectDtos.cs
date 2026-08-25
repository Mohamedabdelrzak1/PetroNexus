using System;
using System.Collections.Generic;

namespace Shared.Dto.EngineeringProjects
{
    public class EngineeringProjectCreateDto
    {
        public int? TenderId { get; set; }
        public string Title { get; set; } = null!;
        public string? ScopeDescription { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

    public class EngineeringProjectUpdateDto
    {
        public int? TenderId { get; set; }
        public string Title { get; set; } = null!;
        public string? ScopeDescription { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

    public class EngineeringProjectResponseDto
    {
        public int Id { get; set; }
        public int? TenderId { get; set; }
        public string? TenderTitle { get; set; }
        public string Title { get; set; } = null!;
        public string? ScopeDescription { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

    public class EngineeringProjectDetailsDto : EngineeringProjectResponseDto
    {
        public List<EngineeringDeliverableResponseDto> Deliverables { get; set; } = new();
    }
}

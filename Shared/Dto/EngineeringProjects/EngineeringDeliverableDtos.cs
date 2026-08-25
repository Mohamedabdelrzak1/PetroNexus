using System;
using Domain.Enums;

namespace Shared.Dto.EngineeringProjects
{
    public class EngineeringDeliverableCreateDto
    {
        public int EngineeringProjectId { get; set; }
        public string Name { get; set; } = null!;
        public int RevisionNumber { get; set; }
        public DeliverableStatus Status { get; set; }
        public string? FilePath { get; set; }
        public DateTime? SubmittedAt { get; set; }
        public DateTime? ApprovedAt { get; set; }
        public string? ReviewerComments { get; set; }
    }

    public class EngineeringDeliverableUpdateDto
    {
        public int EngineeringProjectId { get; set; }
        public string Name { get; set; } = null!;
        public int RevisionNumber { get; set; }
        public DeliverableStatus Status { get; set; }
        public string? FilePath { get; set; }
        public DateTime? SubmittedAt { get; set; }
        public DateTime? ApprovedAt { get; set; }
        public string? ReviewerComments { get; set; }
    }

    public class EngineeringDeliverableResponseDto
    {
        public int Id { get; set; }
        public int EngineeringProjectId { get; set; }
        public string EngineeringProjectTitle { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int RevisionNumber { get; set; }
        public DeliverableStatus Status { get; set; }
        public string? FilePath { get; set; }
        public DateTime? SubmittedAt { get; set; }
        public DateTime? ApprovedAt { get; set; }
        public string? ReviewerComments { get; set; }
    }
}

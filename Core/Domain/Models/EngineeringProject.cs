using System;
using System.Collections.Generic;
using Domain.Common;
using Domain.Enums;

namespace Domain.Models
{
    public class EngineeringProject : BaseEntity<int>
    {
        public int? TenderId { get; set; }
        public Tender? Tender { get; set; }

        public string Title { get; set; }
        public string? ScopeDescription { get; set; }

        public DateTime StartDate { get; set; } = DateTime.UtcNow;
        public DateTime? EndDate { get; set; }

        public ICollection<EngineeringDeliverable> Deliverables { get; set; } = new List<EngineeringDeliverable>();
    }

    public class EngineeringDeliverable : BaseEntity<int>
    {
        public int EngineeringProjectId { get; set; }
        public EngineeringProject EngineeringProject { get; set; }

        public string Name { get; set; } // Basic Engineering, 3D Model, Process Simulation...
        public int RevisionNumber { get; set; } = 0;

        public DeliverableStatus Status { get; set; } = DeliverableStatus.Draft;

        public string? FilePath { get; set; }
        public DateTime? SubmittedAt { get; set; }
        public DateTime? ApprovedAt { get; set; }
        public string? ReviewerComments { get; set; }
    }
}

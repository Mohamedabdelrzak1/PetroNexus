using System;
using Domain.Common;

namespace Domain.Models
{
    // تقييم دوري لأداء الـ Principal — بيغذي PerformanceScore الإجمالي على كيان Principal
    public class PrincipalPerformanceReview : BaseEntity<int>
    {
        public int PrincipalId { get; set; }
        public Principal Principal { get; set; }

        public DateTime ReviewDate { get; set; } = DateTime.UtcNow;

        public double ResponseSpeedScore { get; set; }       // 0-5
        public double DeliveryCommitmentScore { get; set; }  // 0-5
        public double QualityScore { get; set; }              // 0-5

        public string? Notes { get; set; }

        public string? ReviewedByUserId { get; set; }
        public AppUser? ReviewedByUser { get; set; }

        public double AverageScore => (ResponseSpeedScore + DeliveryCommitmentScore + QualityScore) / 3;
    }
}

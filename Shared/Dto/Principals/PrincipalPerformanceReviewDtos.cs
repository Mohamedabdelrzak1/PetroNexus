using System;

namespace Shared.Dto.Principals
{
    public class PrincipalPerformanceReviewCreateDto
    {
        public int PrincipalId { get; set; }
        public DateTime ReviewDate { get; set; }
        public double QualityScore { get; set; }
        public double DeliveryScore { get; set; }
        public double SupportScore { get; set; }
        public double OverallScore { get; set; }
        public string? Comments { get; set; }
    }

    public class PrincipalPerformanceReviewUpdateDto
    {
        public int PrincipalId { get; set; }
        public DateTime ReviewDate { get; set; }
        public double QualityScore { get; set; }
        public double DeliveryScore { get; set; }
        public double SupportScore { get; set; }
        public double OverallScore { get; set; }
        public string? Comments { get; set; }
    }

    public class PrincipalPerformanceReviewResponseDto
    {
        public int Id { get; set; }
        public int PrincipalId { get; set; }
        public string PrincipalName { get; set; } = null!;
        public DateTime ReviewDate { get; set; }
        public double QualityScore { get; set; }
        public double DeliveryScore { get; set; }
        public double SupportScore { get; set; }
        public double OverallScore { get; set; }
        public string? Comments { get; set; }
    }
}

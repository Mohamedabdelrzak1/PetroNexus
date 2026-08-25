using System;
using System.Collections.Generic;
using Shared.Dto.Commissions;

namespace Shared.Dto.Principals
{
    public class PrincipalCreateDto
    {
        public string Name { get; set; } = null!;
        public string? Country { get; set; }
        public string? Website { get; set; }
        public string? AgencyAgreementPath { get; set; }
        public DateTime AgreementExpiryDate { get; set; }
        public double PerformanceScore { get; set; }
    }

    public class PrincipalUpdateDto
    {
        public string Name { get; set; } = null!;
        public string? Country { get; set; }
        public string? Website { get; set; }
        public string? AgencyAgreementPath { get; set; }
        public DateTime AgreementExpiryDate { get; set; }
        public double PerformanceScore { get; set; }
    }

    public class PrincipalResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Country { get; set; }
        public string? Website { get; set; }
        public string? AgencyAgreementPath { get; set; }
        public DateTime AgreementExpiryDate { get; set; }
        public double PerformanceScore { get; set; }
    }

    public class PrincipalDetailsDto : PrincipalResponseDto
    {
        public List<PrincipalContactResponseDto> Contacts { get; set; } = new();
        public List<PrincipalProductResponseDto> Products { get; set; } = new();
        public List<PrincipalPerformanceReviewResponseDto> PerformanceReviews { get; set; } = new();
        public List<CommissionResponseDto> Commissions { get; set; } = new();
    }
}

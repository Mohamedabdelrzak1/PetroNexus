using System;
using System.Collections.Generic;
using Domain.Common;
using Domain.Enums;

namespace Domain.Models
{
    // الشركة المصنّعة العالمية اللي إحنا بنمثلها (Agency)
    public class Principal : BaseEntity<int>
    {
        public string Name { get; set; }
        public string? Country { get; set; }
        public string? AgreementReference { get; set; }

        public DateTime? AgreementStartDate { get; set; }
        public DateTime? AgreementEndDate { get; set; }

        public bool IsExclusiveTerritory { get; set; } = false;

        public decimal CommissionPercentage { get; set; }

        // تقييم أداء دوري من 0 إلى 5 (سرعة رد، التزام بمواعيد، جودة)
        public double PerformanceScore { get; set; } = 0;

        public ICollection<PrincipalProduct> Products { get; set; } = new List<PrincipalProduct>();
        public ICollection<PrincipalContact> Contacts { get; set; } = new List<PrincipalContact>();

        // 🆕 تقييمات دورية تفصيلية بتغذي PerformanceScore
        public ICollection<PrincipalPerformanceReview> PerformanceReviews { get; set; } = new List<PrincipalPerformanceReview>();

        // 🆕 العمولات المستحقة من هذا الـ Principal
        public ICollection<Commission> Commissions { get; set; } = new List<Commission>();
    }

    public class PrincipalProduct : BaseEntity<int>
    {
        public int PrincipalId { get; set; }
        public Principal Principal { get; set; }

        public string Name { get; set; }
        public string? Category { get; set; } // Heat Transfer, Heavy Equipment, Skid Package, Pipes, Valves...

        public decimal ReferencePrice { get; set; }
        public CurrencyType Currency { get; set; } = CurrencyType.USD;

        public string? Specifications { get; set; }
    }
}

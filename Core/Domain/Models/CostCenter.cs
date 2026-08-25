using System.Collections.Generic;
using Domain.Common;

namespace Domain.Models
{
    // مركز تكلفة لكل مشروع/مناقصة — بيتجمّع عليه كل القيود (تكاليف وإيرادات)
    // عشان نعرف ربحية المشروع الفعلية مش التقديرية بس
    public class CostCenter : BaseEntity<int>
    {
        public string Code { get; set; }
        public string Name { get; set; }

        public int? TenderId { get; set; }
        public Tender? Tender { get; set; }

        public ICollection<JournalEntryLine> JournalEntryLines { get; set; } = new List<JournalEntryLine>();
    }
}

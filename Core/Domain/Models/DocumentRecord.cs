using System;
using System.Collections.Generic;
using Domain.Common;
using Domain.Enums;

namespace Domain.Models
{
    // أرشيف موحد لأي مستند في النظام، مربوط بأي كيان (Tender, Client, Principal, Shipment...)
    public class DocumentRecord : BaseEntity<int>
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }

        public DocumentCategory Category { get; set; }

        public string RelatedEntityType { get; set; } // اسم الكيان: "Tender", "Client"...
        public int RelatedEntityId { get; set; }

        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
        public string? UploadedByUserId { get; set; }
        public AppUser? UploadedByUser { get; set; }

        // 🆕 تواقيع إلكترونية على المستند
        public ICollection<DocumentSignature> Signatures { get; set; } = new List<DocumentSignature>();
    }
}

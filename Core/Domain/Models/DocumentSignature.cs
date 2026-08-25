using System;
using Domain.Common;
using Domain.Enums;

namespace Domain.Models
{
    // توقيع إلكتروني على مستند (عرض فني/مالي/عقد) بدل الطباعة والتوقيع اليدوي
    public class DocumentSignature : BaseEntity<int>
    {
        public int DocumentRecordId { get; set; }
        public DocumentRecord DocumentRecord { get; set; }

        public string SignerUserId { get; set; }
        public AppUser SignerUser { get; set; }

        public SignatureStatus Status { get; set; } = SignatureStatus.Pending;

        public string? SignatureImagePath { get; set; }
        public string? SignatureHash { get; set; }   // لضمان عدم التلاعب بالمستند بعد التوقيع

        public DateTime RequestedAt { get; set; } = DateTime.UtcNow;
        public DateTime? SignedAt { get; set; }
        public string? IpAddress { get; set; }

        // 🆕 سبب الرفض عند رفض التوقيع
        public string? RejectionReason { get; set; }
    }
}

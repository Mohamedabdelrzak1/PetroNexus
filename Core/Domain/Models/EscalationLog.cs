using System;
using Domain.Common;
using Domain.Enums;

namespace Domain.Models
{
    // سجل التنبيهات المُصعَّدة (تجديد تسجيل قارب على الانتهاء، خطر تأخير شحنة...) عبر أي قناة
    public class EscalationLog : BaseEntity<int>
    {
        public string RelatedEntityType { get; set; }   // "Shipment", "VendorRegistration"...
        public int RelatedEntityId { get; set; }

        public EscalationLevel Level { get; set; } = EscalationLevel.Warning;
        public NotificationChannel Channel { get; set; } = NotificationChannel.InApp;

        public string Message { get; set; }

        public string? SentToUserId { get; set; }
        public AppUser? SentToUser { get; set; }

        public DateTime SentAt { get; set; } = DateTime.UtcNow;
        public bool WasAcknowledged { get; set; } = false;
    }
}

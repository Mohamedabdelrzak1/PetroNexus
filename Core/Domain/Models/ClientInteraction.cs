using System;
using Domain.Common;
using Domain.Enums;

namespace Domain.Models
{
    // سجل كل تفاعل مع العميل: مكالمة، اجتماع، إيميل، زيارة
    public class ClientInteraction : BaseEntity<int>
    {
        public int ClientId { get; set; }
        public Client Client { get; set; }

        public int? TenderId { get; set; }
        public Tender? Tender { get; set; }

        public InteractionType Type { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;

        public string? Notes { get; set; }

        public string? CreatedByUserId { get; set; }
        public AppUser? CreatedByUser { get; set; }
    }
}

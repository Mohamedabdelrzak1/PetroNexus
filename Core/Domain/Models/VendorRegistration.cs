using System;
using System.Collections.Generic;
using Domain.Common;
using Domain.Enums;

namespace Domain.Models
{
    // حالة تسجيل شركتنا كمورد معتمد لدى عميل معين (EGPC/ENPPI...)
    public class VendorRegistration : BaseEntity<int>
    {
        public int ClientId { get; set; }
        public Client Client { get; set; }

        public string RegistrationNumber { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime ExpiryDate { get; set; }

        public RegistrationStatus Status { get; set; } = RegistrationStatus.Active;

        public ICollection<RegistrationDocument> Documents { get; set; } = new List<RegistrationDocument>();
    }

    public class RegistrationDocument : BaseEntity<int>
    {
        public int VendorRegistrationId { get; set; }
        public VendorRegistration VendorRegistration { get; set; }

        public string DocumentName { get; set; }
        public string FilePath { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }
}

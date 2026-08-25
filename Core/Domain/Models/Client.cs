using System;
using System.Collections.Generic;
using Domain.Common;

namespace Domain.Models
{
    // العميل: EGPC, ENPPI, Petrobel, TAQA...
    public class Client : BaseEntity<int>
    {
        public string Name { get; set; }
        public string? NameAr { get; set; }
        public string Sector { get; set; } // Oil & Gas, Petrochemical, Power...

        public string? Address { get; set; }
        public string? TaxNumber { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<ClientContact> Contacts { get; set; } = new List<ClientContact>();
        public ICollection<Tender> Tenders { get; set; } = new List<Tender>();
        public ICollection<VendorRegistration> Registrations { get; set; } = new List<VendorRegistration>();
        public ICollection<ClientInteraction> Interactions { get; set; } = new List<ClientInteraction>();
        public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
        public ICollection<ClientPortalUser> PortalUsers { get; set; } = new List<ClientPortalUser>();
    }

    public class ClientContact : BaseEntity<int>
    {
        public int ClientId { get; set; }
        public Client Client { get; set; }

        public string FullName { get; set; }
        public string? JobTitle { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public bool IsPrimary { get; set; } = false;
    }
}

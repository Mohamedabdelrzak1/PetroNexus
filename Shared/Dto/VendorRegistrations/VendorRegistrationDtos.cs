using System;
using System.Collections.Generic;
using Domain.Enums;

namespace Shared.Dto.VendorRegistrations
{
    public class VendorRegistrationCreateDto
    {
        public int ClientId { get; set; }
        public string RegistrationNumber { get; set; } = null!;
        public DateTime RegistrationDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public RegistrationStatus Status { get; set; }
    }

    public class VendorRegistrationUpdateDto
    {
        public int ClientId { get; set; }
        public string RegistrationNumber { get; set; } = null!;
        public DateTime RegistrationDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public RegistrationStatus Status { get; set; }
    }

    public class VendorRegistrationResponseDto
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; } = null!;
        public string RegistrationNumber { get; set; } = null!;
        public DateTime RegistrationDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public RegistrationStatus Status { get; set; }
    }

    public class VendorRegistrationDetailsDto : VendorRegistrationResponseDto
    {
        public List<RegistrationDocumentResponseDto> Documents { get; set; } = new();
    }

    public class RegistrationDocumentResponseDto
    {
        public int Id { get; set; }
        public int VendorRegistrationId { get; set; }
        public string DocumentName { get; set; } = null!;
        public string FilePath { get; set; } = null!;
        public DateTime? ExpiryDate { get; set; }
    }
}

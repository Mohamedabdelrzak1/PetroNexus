using System;

namespace Shared.Dto.RegistrationDocuments
{
    public class RegistrationDocumentCreateDto
    {
        public int VendorRegistrationId { get; set; }
        public string DocumentName { get; set; } = null!;
        public string FilePath { get; set; } = null!;
        public DateTime? ExpiryDate { get; set; }
    }

    public class RegistrationDocumentUpdateDto
    {
        public int VendorRegistrationId { get; set; }
        public string DocumentName { get; set; } = null!;
        public string FilePath { get; set; } = null!;
        public DateTime? ExpiryDate { get; set; }
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

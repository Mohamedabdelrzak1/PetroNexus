namespace Domain.Enums
{
    // =========================
    // 💰 HR / Payroll
    // =========================
    public enum SalaryStatus
    {
        Draft = 1,
        Approved = 2,
        Paid = 3
    }

    // =========================
    // 📄 Tender & CRM
    // =========================
    public enum TenderStatus
    {
        New = 1,
        UnderPreparation = 2,
        Submitted = 3,
        Won = 4,
        Lost = 5,
        Cancelled = 6
    }

    public enum QuotationStatus
    {
        Draft = 1,
        SentToClient = 2,
        Accepted = 3,
        Rejected = 4,
        Expired = 5
    }

    // =========================
    // 🏷️ Vendor Registration
    // =========================
    public enum RegistrationStatus
    {
        Active = 1,
        PendingRenewal = 2,
        Expired = 3,
        Suspended = 4
    }

    // =========================
    // 🚢 Import & Logistics
    // =========================
    public enum ShipmentStatus
    {
        AwaitingFabrication = 1,
        InFabrication = 2,
        ReadyForShipment = 3,
        Shipped = 4,
        InCustoms = 5,
        Delivered = 6,
        Delayed = 7
    }

    // =========================
    // 🛠️ Fabrication & QA/QC
    // =========================
    public enum InspectionResult
    {
        Pending = 1,
        Passed = 2,
        Failed = 3,
        ConditionalPass = 4
    }

    // =========================
    // 📐 Engineering
    // =========================
    public enum DeliverableStatus
    {
        Draft = 1,
        UnderReview = 2,
        Approved = 3,
        Rejected = 4,
        Superseded = 5
    }

    // =========================
    // 📁 Documents
    // =========================
    public enum DocumentCategory
    {
        TechnicalDatasheet = 1,
        QualityCertificate = 2,
        Correspondence = 3,
        Contract = 4,
        Invoice = 5,
        Other = 6
    }

    // =========================
    // 💵 Finance
    // =========================
    public enum CurrencyType
    {
        EGP = 1,
        USD = 2,
        EUR = 3
    }

    public enum AccountType
    {
        Asset = 1,
        Liability = 2,
        Equity = 3,
        Revenue = 4,
        Expense = 5
    }

    // =========================
    // 🔔 Notifications
    // =========================
    public enum NotificationChannel
    {
        InApp = 1,
        Email = 2,
        WhatsApp = 3
    }

    // =========================
    // 🧾 Invoicing
    // =========================
    public enum InvoiceStatus
    {
        Draft = 1,
        Sent = 2,
        PartiallyPaid = 3,
        Paid = 4,
        Overdue = 5,
        Cancelled = 6,
        Approved = 7
    }

    // =========================
    // 🕒 HR - Attendance/Leave
    // =========================
    public enum LeaveType
    {
        Annual = 1,
        Sick = 2,
        Unpaid = 3,
        Other = 4
    }

    // =========================
    // 🧭 CRM
    // =========================
    public enum InteractionType
    {
        Call = 1,
        Meeting = 2,
        Email = 3,
        Visit = 4
    }

    // =========================
    // 🕵️ Audit
    // =========================
    public enum AuditAction
    {
        Create = 1,
        Update = 2,
        Delete = 3
    }

    // =========================
    // 🔐 Permissions 🆕
    // =========================
    public enum PermissionScope
    {
        View = 1,
        Create = 2,
        Edit = 3,
        Delete = 4,
        Approve = 5
    }

    // =========================
    // ✍️ E-Signature 🆕
    // =========================
    public enum SignatureStatus
    {
        Pending = 1,
        Signed = 2,
        Rejected = 3,
        Expired = 4
    }

    // =========================
    // 💸 Commission 🆕
    // =========================
    public enum CommissionStatus
    {
        Accrued = 1,
        Invoiced = 2,
        Received = 3
    }

    // =========================
    // 🚨 Escalation 🆕
    // =========================
    public enum EscalationLevel
    {
        Info = 1,
        Warning = 2,
        Critical = 3
    }

    // =========================
    // 🌐 Tender Source 🆕
    // =========================
    public enum TenderSource
    {
        Manual = 1,
        MinistryPortalScraper = 2,
        AiExtracted = 3
    }

    // =========================
    // ⚖️ Liquidated Damages 🆕
    // =========================
    public enum LdStatus
    {
        Threatened = 1,
        Applied = 2,
        Waived = 3,
        Disputed = 4
    }
}

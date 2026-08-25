using ServiceAbstraction.IAuthServices;
using ServiceAbstraction.IUser;
using ServiceAbstraction.IClients;
using ServiceAbstraction.ITenders;
using ServiceAbstraction.IQuotations;
using ServiceAbstraction.IPrincipals;
using ServiceAbstraction.ICommissions;
using ServiceAbstraction.IVendorRegistrations;
using ServiceAbstraction.IPurchaseOrders;
using ServiceAbstraction.ILettersOfCredit;
using ServiceAbstraction.IShipments;
using ServiceAbstraction.ILiquidatedDamages;
using ServiceAbstraction.IFabricationOrders;
using ServiceAbstraction.IQualityInspections;
using ServiceAbstraction.IEngineeringProjects;
using ServiceAbstraction.IDepartments;
using ServiceAbstraction.IEmployees;
using ServiceAbstraction.IAttendances;
using ServiceAbstraction.ILeaveRequests;
using ServiceAbstraction.ISalaries;
using ServiceAbstraction.IAccounts;
using ServiceAbstraction.ICostCenters;
using ServiceAbstraction.IJournalEntries;
using ServiceAbstraction.IInvoices;
using ServiceAbstraction.IPayments;
using ServiceAbstraction.IExchangeRates;
using ServiceAbstraction.ICompanySettings;
using ServiceAbstraction.IPermissions;
using ServiceAbstraction.IDocumentRecords;
using ServiceAbstraction.INotifications;
using ServiceAbstraction.IEscalationLogs;
using ServiceAbstraction.IAuditLogs;
using ServiceAbstraction.IInventory;
using ServiceAbstraction.IJournalPosting;
using ServiceAbstraction.IDashboard;

namespace ServiceAbstraction
{
    public interface IServiceManager
    {
        // ===================== Auth =====================
        IAuthService AuthService { get; }
        IUserService UserService { get; }

        // ===================== CRM - Clients =====================
        IClientService ClientService { get; }
        IClientContactService ClientContactService { get; }
        IClientInteractionService ClientInteractionService { get; }
        IClientPortalUserService ClientPortalUserService { get; }

        // ===================== CRM - Tenders =====================
        ITenderService TenderService { get; }
        ITenderItemService TenderItemService { get; }
        ITenderLeadService TenderLeadService { get; }
        ITenderDocumentAnalysisService TenderDocumentAnalysisService { get; }

        // ===================== CRM - Quotations =====================
        IQuotationService QuotationService { get; }
        IQuotationItemService QuotationItemService { get; }

        // ===================== Agency - Principals =====================
        IPrincipalService PrincipalService { get; }
        IPrincipalContactService PrincipalContactService { get; }
        IPrincipalProductService PrincipalProductService { get; }
        IPrincipalPerformanceReviewService PrincipalPerformanceReviewService { get; }

        // ===================== Agency - Commissions =====================
        ICommissionService CommissionService { get; }

        // ===================== Agency - Vendor Registrations =====================
        IVendorRegistrationService VendorRegistrationService { get; }
        IRegistrationDocumentService RegistrationDocumentService { get; }

        // ===================== Logistics - Purchase Orders =====================
        IPurchaseOrderService PurchaseOrderService { get; }
        IPurchaseOrderItemService PurchaseOrderItemService { get; }

        // ===================== Logistics - Letters of Credit =====================
        ILetterOfCreditService LetterOfCreditService { get; }

        // ===================== Logistics - Shipments =====================
        IShipmentService ShipmentService { get; }
        IShipmentTrackingEventService ShipmentTrackingEventService { get; }

        // ===================== Logistics - Liquidated Damages =====================
        ILiquidatedDamageService LiquidatedDamageService { get; }

        // ===================== Fabrication & QC =====================
        IFabricationOrderService FabricationOrderService { get; }
        IQualityInspectionService QualityInspectionService { get; }
        INonConformanceReportService NonConformanceReportService { get; }

        // ===================== Engineering =====================
        IEngineeringProjectService EngineeringProjectService { get; }
        IEngineeringDeliverableService EngineeringDeliverableService { get; }

        // ===================== HR =====================
        IDepartmentService DepartmentService { get; }
        IEmployeeService EmployeeService { get; }
        IEmployeeDocumentService EmployeeDocumentService { get; }
        IAttendanceService AttendanceService { get; }
        ILeaveRequestService LeaveRequestService { get; }
        ISalaryService SalaryService { get; }

        // ===================== Finance =====================
        IAccountService AccountService { get; }
        ICostCenterService CostCenterService { get; }
        IJournalEntryService JournalEntryService { get; }
        IJournalEntryLineService JournalEntryLineService { get; }
        IInvoiceService InvoiceService { get; }
        IInvoiceItemService InvoiceItemService { get; }
        IPaymentService PaymentService { get; }
        IExchangeRateService ExchangeRateService { get; }
        ICompanySettingsService CompanySettingsService { get; }

        // ===================== Security & Access =====================
        IPermissionService PermissionService { get; }
        IRolePermissionService RolePermissionService { get; }
        IRefreshTokenService RefreshTokenService { get; }

        // ===================== Documents =====================
        IDocumentRecordService DocumentRecordService { get; }
        IDocumentSignatureService DocumentSignatureService { get; }

        // ===================== Notifications =====================
        INotificationService NotificationService { get; }
        IEscalationLogService EscalationLogService { get; }

        // ===================== Audit & Inventory =====================
        IAuditLogService AuditLogService { get; }
        IInventoryService InventoryService { get; }

        // ===================== Journal Posting =====================
        IJournalPostingService JournalPostingService { get; }

        // ===================== Dashboard & Analytics =====================
        IDashboardService DashboardService { get; }
    }
}

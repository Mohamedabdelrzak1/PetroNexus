using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using Service.Auth;
using Service.Helpers;
using Service.Hubs;
using ServiceAbstraction;
using Service.Services.Accounts;
using Service.Services.Attendances;
using Service.Services.CostCenters;
using Service.Services.Clients;
using Service.Services.Commissions;
using Service.Services.Departments;
using Service.Services.DocumentRecords;
using Service.Services.Employees;
using Service.Services.EngineeringProjects;
using Service.Services.EscalationLogs;
using Service.Services.ExchangeRates;
using Service.Services.FabricationOrders;
using Service.Services.Invoices;
using Service.Services.JournalEntries;
using Service.Services.LeaveRequests;
using Service.Services.LettersOfCredit;
using Service.Services.LiquidatedDamages;
using Service.Services.Notifications;
using Service.Services.Payments;
using Service.Services.Permissions;
using Service.Services.Principals;
using Service.Services.PurchaseOrders;
using Service.Services.QualityInspections;
using Service.Services.Quotations;
using Service.Services.Salaries;
using Service.Services.Shipments;
using Service.Services.Tenders;
using Service.Services.VendorRegistrations;
using Service.User;
using ServiceAbstraction;
using ServiceAbstraction.IAccounts;
using ServiceAbstraction.IAttendances;
using ServiceAbstraction.IAuditLogs;
using ServiceAbstraction.IAuthServices;
using ServiceAbstraction.IClients;
using ServiceAbstraction.ICommissions;
using ServiceAbstraction.ICompanySettings;
using ServiceAbstraction.ICostCenters;
using ServiceAbstraction.IDepartments;
using ServiceAbstraction.IDocumentRecords;
using ServiceAbstraction.IEmployees;
using ServiceAbstraction.IEngineeringProjects;
using ServiceAbstraction.IEscalationLogs;
using ServiceAbstraction.IExchangeRates;
using ServiceAbstraction.IFabricationOrders;
using ServiceAbstraction.IInventory;
using ServiceAbstraction.IInvoices;
using ServiceAbstraction.IJournalEntries;
using ServiceAbstraction.ILeaveRequests;
using ServiceAbstraction.ILettersOfCredit;
using ServiceAbstraction.ILiquidatedDamages;
using ServiceAbstraction.INotifications;
using ServiceAbstraction.IPayments;
using ServiceAbstraction.IPermissions;
using ServiceAbstraction.IPrincipals;
using ServiceAbstraction.IPurchaseOrders;
using ServiceAbstraction.IQualityInspections;
using ServiceAbstraction.IQuotations;
using ServiceAbstraction.ISalaries;
using ServiceAbstraction.IShipments;
using ServiceAbstraction.ITenders;
using ServiceAbstraction.IUser;
using ServiceAbstraction.IVendorRegistrations;
using ServiceAbstraction.IJournalPosting;
using Service.Services.JournalPosting;
using ServiceAbstraction.IDashboard;
using Service.Services.Dashboard;
using Shared.Dto.Accounts;
using Shared.Dto.Attendances;
using Shared.Dto.Clients;
using Shared.Dto.Commissions;
using Shared.Dto.CostCenters;
using Shared.Dto.Departments;
using Shared.Dto.DocumentRecords;
using Shared.Dto.DocumentSignatures;
using Shared.Dto.EngineeringProjects;
using Shared.Dto.EscalationLogs;
using Shared.Dto.ExchangeRates;
using Shared.Dto.FabricationOrders;
using Shared.Dto.Invoices;
using Shared.Dto.JournalEntries;
using Shared.Dto.LeaveRequests;
using Shared.Dto.LettersOfCredit;
using Shared.Dto.LiquidatedDamages;
using Shared.Dto.NonConformanceReports;
using Shared.Dto.Notifications;
using Shared.Dto.Payments;
using Shared.Dto.Principals;
using Shared.Dto.PurchaseOrders;
using Shared.Dto.Quotations;
using Shared.Dto.RegistrationDocuments;
using Shared.Dto.RolePermissions;
using Shared.Dto.Salaries;
using Shared.Dto.Shipments;
using Shared.Dto.Tenders;
using Shared.Dto.VendorRegistrations;
using Shared.Dto.Auth;
using Service.Services.CompanySettingss;
using Shared.Dto.AuditLogs;
using Shared.Dto.CompanySettings;
using Shared.Dto.Employees;

namespace Service
{
    public class ServiceManager : IServiceManager
    {
        // ── Auth ──────────────────────────────────────────────────────────
        private readonly Lazy<IAuthService> _authService;
        private readonly Lazy<IUserService> _userService;

        // ── Clients ───────────────────────────────────────────────────────
        private readonly Lazy<IClientService> _clientService;
        private readonly Lazy<IClientContactService> _clientContactService;
        private readonly Lazy<IClientInteractionService> _clientInteractionService;
        private readonly Lazy<IClientPortalUserService> _clientPortalUserService;

        // ── Tenders ───────────────────────────────────────────────────────
        private readonly Lazy<ITenderService> _tenderService;
        private readonly Lazy<ITenderItemService> _tenderItemService;
        private readonly Lazy<ITenderLeadService> _tenderLeadService;
        private readonly Lazy<ITenderDocumentAnalysisService> _tenderDocumentAnalysisService;

        // ── Quotations ────────────────────────────────────────────────────
        private readonly Lazy<IQuotationService> _quotationService;
        private readonly Lazy<IQuotationItemService> _quotationItemService;

        // ── Principals ────────────────────────────────────────────────────
        private readonly Lazy<IPrincipalService> _principalService;
        private readonly Lazy<IPrincipalContactService> _principalContactService;
        private readonly Lazy<IPrincipalProductService> _principalProductService;
        private readonly Lazy<IPrincipalPerformanceReviewService> _principalPerformanceReviewService;

        // ── Commissions ───────────────────────────────────────────────────
        private readonly Lazy<ICommissionService> _commissionService;

        // ── Vendor Registrations ──────────────────────────────────────────
        private readonly Lazy<IVendorRegistrationService> _vendorRegistrationService;
        private readonly Lazy<IRegistrationDocumentService> _registrationDocumentService;

        // ── Purchase Orders ───────────────────────────────────────────────
        private readonly Lazy<IPurchaseOrderService> _purchaseOrderService;
        private readonly Lazy<IPurchaseOrderItemService> _purchaseOrderItemService;

        // ── Letters of Credit ─────────────────────────────────────────────
        private readonly Lazy<ILetterOfCreditService> _letterOfCreditService;

        // ── Shipments ─────────────────────────────────────────────────────
        private readonly Lazy<IShipmentService> _shipmentService;
        private readonly Lazy<IShipmentTrackingEventService> _shipmentTrackingEventService;

        // ── Liquidated Damages ────────────────────────────────────────────
        private readonly Lazy<ILiquidatedDamageService> _liquidatedDamageService;

        // ── Fabrication & QC ──────────────────────────────────────────────
        private readonly Lazy<IFabricationOrderService> _fabricationOrderService;
        private readonly Lazy<IQualityInspectionService> _qualityInspectionService;
        private readonly Lazy<INonConformanceReportService> _nonConformanceReportService;

        // ── Engineering ───────────────────────────────────────────────────
        private readonly Lazy<IEngineeringProjectService> _engineeringProjectService;
        private readonly Lazy<IEngineeringDeliverableService> _engineeringDeliverableService;

        // ── HR ────────────────────────────────────────────────────────────
        private readonly Lazy<IDepartmentService> _departmentService;
        private readonly Lazy<IEmployeeService> _employeeService;
        private readonly Lazy<IEmployeeDocumentService> _employeeDocumentService;
        private readonly Lazy<IAttendanceService> _attendanceService;
        private readonly Lazy<ILeaveRequestService> _leaveRequestService;
        private readonly Lazy<ISalaryService> _salaryService;

        // ── Finance ───────────────────────────────────────────────────────
        private readonly Lazy<IAccountService> _accountService;
        private readonly Lazy<ICostCenterService> _costCenterService;
        private readonly Lazy<IJournalEntryService> _journalEntryService;
        private readonly Lazy<IJournalEntryLineService> _journalEntryLineService;
        private readonly Lazy<IInvoiceService> _invoiceService;
        private readonly Lazy<IInvoiceItemService> _invoiceItemService;
        private readonly Lazy<IPaymentService> _paymentService;
        private readonly Lazy<IExchangeRateService> _exchangeRateService;
        private readonly Lazy<ICompanySettingsService> _companySettingsService;

        // ── Security ──────────────────────────────────────────────────────
        private readonly Lazy<IPermissionService> _permissionService;
        private readonly Lazy<IRolePermissionService> _rolePermissionService;
        private readonly Lazy<IRefreshTokenService> _refreshTokenService;

        // ── Documents ─────────────────────────────────────────────────────
        private readonly Lazy<IDocumentRecordService> _documentRecordService;
        private readonly Lazy<IDocumentSignatureService> _documentSignatureService;

        // ── Notifications ─────────────────────────────────────────────────
        private readonly Lazy<INotificationService> _notificationService;
        private readonly Lazy<IEscalationLogService> _escalationLogService;

        // ── Audit & Inventory ─────────────────────────────────────────────
        private readonly Lazy<IAuditLogService> _auditLogService;
        private readonly Lazy<IInventoryService> _inventoryService;

        // ── Journal Posting ───────────────────────────────────────────────
        private readonly Lazy<IJournalPostingService> _journalPostingService;

        // ── Dashboard & Analytics ─────────────────────────────────────────
        private readonly Lazy<IDashboardService> _dashboardService;

        public ServiceManager(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager,
            IOptions<JwtOptions> options,
            IHttpContextAccessor httpContextAccessor,
            IMailingService mailService,
            IHubContext<NotificationHub> hubContext,
            IJournalPostingService journalPostingService = null,
            IServiceProvider serviceProvider = null)
        {
            // Auth
            _authService = new Lazy<IAuthService>(() => new AuthService(userManager, options, mailService));
            _userService = new Lazy<IUserService>(() => new UserService(userManager, roleManager, mapper));

            // Helper to resolve validators
            IValidator<T>? ResolveValidator<T>() => serviceProvider?.GetService<IValidator<T>>();

            // Clients
            _clientService = new Lazy<IClientService>(() => new ClientService(unitOfWork, mapper, ResolveValidator<ClientCreateDto>(), ResolveValidator<ClientUpdateDto>()));
            _clientContactService = new Lazy<IClientContactService>(() => new ClientContactService(unitOfWork, mapper, ResolveValidator<ClientContactCreateDto>(), ResolveValidator<ClientContactUpdateDto>()));
            _clientInteractionService = new Lazy<IClientInteractionService>(() => new ClientInteractionService(unitOfWork, mapper, ResolveValidator<ClientInteractionCreateDto>(), ResolveValidator<ClientInteractionUpdateDto>()));
            _clientPortalUserService = new Lazy<IClientPortalUserService>(() => new ClientPortalUserService(unitOfWork, mapper, ResolveValidator<ClientPortalUserCreateDto>(), ResolveValidator<ClientPortalUserUpdateDto>()));

            // Tenders
            _tenderService = new Lazy<ITenderService>(() => new TenderService(unitOfWork, mapper, ResolveValidator<TenderCreateDto>(), ResolveValidator<TenderUpdateDto>()));
            _tenderItemService = new Lazy<ITenderItemService>(() => new TenderItemService(unitOfWork, mapper, ResolveValidator<TenderItemCreateDto>(), ResolveValidator<TenderItemUpdateDto>()));
            _tenderLeadService = new Lazy<ITenderLeadService>(() => new TenderLeadService(unitOfWork, mapper, ResolveValidator<TenderLeadCreateDto>(), ResolveValidator<TenderLeadUpdateDto>()));
            _tenderDocumentAnalysisService = new Lazy<ITenderDocumentAnalysisService>(() => new TenderDocumentAnalysisService(unitOfWork, mapper, ResolveValidator<TenderDocumentAnalysisCreateDto>(), ResolveValidator<TenderDocumentAnalysisUpdateDto>()));

            // Quotations
            _quotationService = new Lazy<IQuotationService>(() => new QuotationService(unitOfWork, mapper, ResolveValidator<QuotationCreateDto>(), ResolveValidator<QuotationUpdateDto>()));
            _quotationItemService = new Lazy<IQuotationItemService>(() => new QuotationItemService(unitOfWork, mapper, ResolveValidator<QuotationItemCreateDto>(), ResolveValidator<QuotationItemUpdateDto>()));

            // Principals
            _principalService = new Lazy<IPrincipalService>(() => new PrincipalService(unitOfWork, mapper, ResolveValidator<PrincipalCreateDto>(), ResolveValidator<PrincipalUpdateDto>()));
            _principalContactService = new Lazy<IPrincipalContactService>(() => new PrincipalContactService(unitOfWork, mapper, ResolveValidator<PrincipalContactCreateDto>(), ResolveValidator<PrincipalContactUpdateDto>()));
            _principalProductService = new Lazy<IPrincipalProductService>(() => new PrincipalProductService(unitOfWork, mapper, ResolveValidator<PrincipalProductCreateDto>(), ResolveValidator<PrincipalProductUpdateDto>()));
            _principalPerformanceReviewService = new Lazy<IPrincipalPerformanceReviewService>(() => new PrincipalPerformanceReviewService(unitOfWork, mapper, ResolveValidator<PrincipalPerformanceReviewCreateDto>(), ResolveValidator<PrincipalPerformanceReviewUpdateDto>()));

            // Commissions
            _commissionService = new Lazy<ICommissionService>(() => new CommissionService(unitOfWork, mapper, ResolveValidator<CommissionCreateDto>(), ResolveValidator<CommissionUpdateDto>()));

            // Vendor Registrations
            _vendorRegistrationService = new Lazy<IVendorRegistrationService>(() => new VendorRegistrationService(unitOfWork, mapper, ResolveValidator<VendorRegistrationCreateDto>(), ResolveValidator<VendorRegistrationUpdateDto>()));
            _registrationDocumentService = new Lazy<IRegistrationDocumentService>(() => new RegistrationDocumentService(unitOfWork, mapper, ResolveValidator<RegistrationDocumentCreateDto>(), ResolveValidator<RegistrationDocumentUpdateDto>()));

            // Purchase Orders
            _purchaseOrderService = new Lazy<IPurchaseOrderService>(() => new PurchaseOrderService(unitOfWork, mapper, ResolveValidator<PurchaseOrderCreateDto>(), ResolveValidator<PurchaseOrderUpdateDto>()));
            _purchaseOrderItemService = new Lazy<IPurchaseOrderItemService>(() => new PurchaseOrderItemService(unitOfWork, mapper, ResolveValidator<PurchaseOrderItemCreateDto>(), ResolveValidator<PurchaseOrderItemUpdateDto>()));

            // Letters of Credit
            _letterOfCreditService = new Lazy<ILetterOfCreditService>(() => new LetterOfCreditService(unitOfWork, mapper, ResolveValidator<LetterOfCreditCreateDto>(), ResolveValidator<LetterOfCreditUpdateDto>()));

            // Shipments
            _shipmentService = new Lazy<IShipmentService>(() => new ShipmentService(unitOfWork, mapper, ResolveValidator<ShipmentCreateDto>(), ResolveValidator<ShipmentUpdateDto>()));
            _shipmentTrackingEventService = new Lazy<IShipmentTrackingEventService>(() => new ShipmentTrackingEventService(unitOfWork, mapper, ResolveValidator<ShipmentTrackingEventCreateDto>(), ResolveValidator<ShipmentTrackingEventUpdateDto>()));

            // Liquidated Damages
            _liquidatedDamageService = new Lazy<ILiquidatedDamageService>(() => new LiquidatedDamageService(unitOfWork, mapper, ResolveValidator<LiquidatedDamageCreateDto>(), ResolveValidator<LiquidatedDamageUpdateDto>()));

            // Fabrication & QC
            _fabricationOrderService = new Lazy<IFabricationOrderService>(() => new FabricationOrderService(unitOfWork, mapper, ResolveValidator<FabricationOrderCreateDto>(), ResolveValidator<FabricationOrderUpdateDto>()));
            _qualityInspectionService = new Lazy<IQualityInspectionService>(() => new QualityInspectionService(unitOfWork, mapper, ResolveValidator<QualityInspectionCreateDto>(), ResolveValidator<QualityInspectionUpdateDto>()));
            _nonConformanceReportService = new Lazy<INonConformanceReportService>(() => new NonConformanceReportService(unitOfWork, mapper, ResolveValidator<NonConformanceReportCreateDto>(), ResolveValidator<NonConformanceReportUpdateDto>()));

            // Engineering
            _engineeringProjectService = new Lazy<IEngineeringProjectService>(() => new EngineeringProjectService(unitOfWork, mapper, ResolveValidator<EngineeringProjectCreateDto>(), ResolveValidator<EngineeringProjectUpdateDto>()));
            _engineeringDeliverableService = new Lazy<IEngineeringDeliverableService>(() => new EngineeringDeliverableService(unitOfWork, mapper, ResolveValidator<EngineeringDeliverableCreateDto>(), ResolveValidator<EngineeringDeliverableUpdateDto>()));

            // HR
            _departmentService = new Lazy<IDepartmentService>(() => new DepartmentService(unitOfWork, mapper, ResolveValidator<DepartmentCreateDto>(), ResolveValidator<DepartmentUpdateDto>()));
            _employeeService = new Lazy<IEmployeeService>(() => new EmployeeService(unitOfWork, mapper, ResolveValidator<EmployeeCreateDto>(), ResolveValidator<EmployeeUpdateDto>()));
            _employeeDocumentService = new Lazy<IEmployeeDocumentService>(() => new EmployeeDocumentService(unitOfWork, mapper, ResolveValidator<EmployeeDocumentCreateDto>(), ResolveValidator<EmployeeDocumentUpdateDto>()));
            _attendanceService = new Lazy<IAttendanceService>(() => new AttendanceService(unitOfWork, mapper, ResolveValidator<AttendanceCreateDto>(), ResolveValidator<AttendanceUpdateDto>()));
            _leaveRequestService = new Lazy<ILeaveRequestService>(() => new LeaveRequestService(unitOfWork, mapper, ResolveValidator<LeaveRequestCreateDto>(), ResolveValidator<LeaveRequestUpdateDto>()));
            _salaryService = new Lazy<ISalaryService>(() => new SalaryService(unitOfWork, mapper, ResolveValidator<SalaryCreateDto>(), ResolveValidator<SalaryUpdateDto>(), journalPostingService));

            // Finance
            _accountService = new Lazy<IAccountService>(() => new AccountService(unitOfWork, mapper, ResolveValidator<AccountCreateDto>(), ResolveValidator<AccountUpdateDto>()));
            _costCenterService = new Lazy<ICostCenterService>(() => new CostCenterService(unitOfWork, mapper, ResolveValidator<CostCenterCreateDto>(), ResolveValidator<CostCenterUpdateDto>()));
            _journalEntryService = new Lazy<IJournalEntryService>(() => new JournalEntryService(unitOfWork, mapper, ResolveValidator<JournalEntryCreateDto>(), ResolveValidator<JournalEntryUpdateDto>()));
            _journalEntryLineService = new Lazy<IJournalEntryLineService>(() => new JournalEntryLineService(unitOfWork, mapper, ResolveValidator<JournalEntryLineCreateDto>(), ResolveValidator<JournalEntryLineUpdateDto>()));
            _invoiceService = new Lazy<IInvoiceService>(() => new InvoiceService(unitOfWork, mapper, ResolveValidator<InvoiceCreateDto>(), ResolveValidator<InvoiceUpdateDto>(), journalPostingService));
            _invoiceItemService = new Lazy<IInvoiceItemService>(() => new InvoiceItemService(unitOfWork, mapper, ResolveValidator<InvoiceItemCreateDto>(), ResolveValidator<InvoiceItemUpdateDto>()));
            _paymentService = new Lazy<IPaymentService>(() => new PaymentService(unitOfWork, mapper, ResolveValidator<PaymentCreateDto>(), ResolveValidator<PaymentUpdateDto>()));
            _exchangeRateService = new Lazy<IExchangeRateService>(() => new ExchangeRateService(unitOfWork, mapper, ResolveValidator<ExchangeRateCreateDto>(), ResolveValidator<ExchangeRateUpdateDto>()));
            _companySettingsService = new Lazy<ICompanySettingsService>(() => new CompanySettingsService(unitOfWork, mapper, ResolveValidator<CompanySettingsCreateDto>(), ResolveValidator<CompanySettingsUpdateDto>()));

            // Security
            _permissionService = new Lazy<IPermissionService>(() => new PermissionService(unitOfWork, mapper, ResolveValidator<PermissionCreateDto>(), ResolveValidator<PermissionUpdateDto>()));
            _rolePermissionService = new Lazy<IRolePermissionService>(() => new RolePermissionService(unitOfWork, mapper, ResolveValidator<RolePermissionCreateDto>(), ResolveValidator<RolePermissionUpdateDto>()));
            _refreshTokenService = new Lazy<IRefreshTokenService>(() => new Service.Services.RefreshTokens.RefreshTokenService(unitOfWork, mapper, ResolveValidator<RefreshTokenCreateDto>(), ResolveValidator<RefreshTokenUpdateDto>()));

            // Documents
            _documentRecordService = new Lazy<IDocumentRecordService>(() => new DocumentRecordService(unitOfWork, mapper, ResolveValidator<DocumentRecordCreateDto>(), ResolveValidator<DocumentRecordUpdateDto>()));
            _documentSignatureService = new Lazy<IDocumentSignatureService>(() => new DocumentSignatureService(unitOfWork, mapper, ResolveValidator<DocumentSignatureCreateDto>(), ResolveValidator<DocumentSignatureUpdateDto>()));

            // Notifications
            _notificationService = new Lazy<INotificationService>(() => new NotificationService(unitOfWork, mapper, ResolveValidator<NotificationCreateDto>(), ResolveValidator<NotificationUpdateDto>()));
            _escalationLogService = new Lazy<IEscalationLogService>(() => new EscalationLogService(unitOfWork, mapper, ResolveValidator<EscalationLogCreateDto>(), ResolveValidator<EscalationLogUpdateDto>()));

            // Audit & Inventory
            _auditLogService = new Lazy<IAuditLogService>(() => new Service.Services.AuditLogs.AuditLogService(unitOfWork, mapper, ResolveValidator<AuditLogCreateDto>(), null));
            _inventoryService = new Lazy<IInventoryService>(() => new Service.Services.Inventory.InventoryService(unitOfWork));

            // Journal Posting
            _journalPostingService = new Lazy<IJournalPostingService>(() => new JournalPostingService(unitOfWork, mapper));

            // Dashboard & Analytics
            _dashboardService = new Lazy<IDashboardService>(() => new DashboardService(unitOfWork));
        }

        // ── Properties ─────────────────────────────────────────────────────
        public IAuthService AuthService => _authService.Value;
        public IUserService UserService => _userService.Value;

        public IClientService ClientService => _clientService.Value;
        public IClientContactService ClientContactService => _clientContactService.Value;
        public IClientInteractionService ClientInteractionService => _clientInteractionService.Value;
        public IClientPortalUserService ClientPortalUserService => _clientPortalUserService.Value;

        public ITenderService TenderService => _tenderService.Value;
        public ITenderItemService TenderItemService => _tenderItemService.Value;
        public ITenderLeadService TenderLeadService => _tenderLeadService.Value;
        public ITenderDocumentAnalysisService TenderDocumentAnalysisService => _tenderDocumentAnalysisService.Value;

        public IQuotationService QuotationService => _quotationService.Value;
        public IQuotationItemService QuotationItemService => _quotationItemService.Value;

        public IPrincipalService PrincipalService => _principalService.Value;
        public IPrincipalContactService PrincipalContactService => _principalContactService.Value;
        public IPrincipalProductService PrincipalProductService => _principalProductService.Value;
        public IPrincipalPerformanceReviewService PrincipalPerformanceReviewService => _principalPerformanceReviewService.Value;

        public ICommissionService CommissionService => _commissionService.Value;

        public IVendorRegistrationService VendorRegistrationService => _vendorRegistrationService.Value;
        public IRegistrationDocumentService RegistrationDocumentService => _registrationDocumentService.Value;

        public IPurchaseOrderService PurchaseOrderService => _purchaseOrderService.Value;
        public IPurchaseOrderItemService PurchaseOrderItemService => _purchaseOrderItemService.Value;

        public ILetterOfCreditService LetterOfCreditService => _letterOfCreditService.Value;

        public IShipmentService ShipmentService => _shipmentService.Value;
        public IShipmentTrackingEventService ShipmentTrackingEventService => _shipmentTrackingEventService.Value;

        public ILiquidatedDamageService LiquidatedDamageService => _liquidatedDamageService.Value;

        public IFabricationOrderService FabricationOrderService => _fabricationOrderService.Value;
        public IQualityInspectionService QualityInspectionService => _qualityInspectionService.Value;
        public INonConformanceReportService NonConformanceReportService => _nonConformanceReportService.Value;

        public IEngineeringProjectService EngineeringProjectService => _engineeringProjectService.Value;
        public IEngineeringDeliverableService EngineeringDeliverableService => _engineeringDeliverableService.Value;

        public IDepartmentService DepartmentService => _departmentService.Value;
        public IEmployeeService EmployeeService => _employeeService.Value;
        public IEmployeeDocumentService EmployeeDocumentService => _employeeDocumentService.Value;
        public IAttendanceService AttendanceService => _attendanceService.Value;
        public ILeaveRequestService LeaveRequestService => _leaveRequestService.Value;
        public ISalaryService SalaryService => _salaryService.Value;

        public IAccountService AccountService => _accountService.Value;
        public ICostCenterService CostCenterService => _costCenterService.Value;
        public IJournalEntryService JournalEntryService => _journalEntryService.Value;
        public IJournalEntryLineService JournalEntryLineService => _journalEntryLineService.Value;
        public IInvoiceService InvoiceService => _invoiceService.Value;
        public IInvoiceItemService InvoiceItemService => _invoiceItemService.Value;
        public IPaymentService PaymentService => _paymentService.Value;
        public IExchangeRateService ExchangeRateService => _exchangeRateService.Value;
        public ICompanySettingsService CompanySettingsService => _companySettingsService.Value;

        public IPermissionService PermissionService => _permissionService.Value;
        public IRolePermissionService RolePermissionService => _rolePermissionService.Value;
        public IRefreshTokenService RefreshTokenService => _refreshTokenService.Value;

        public IDocumentRecordService DocumentRecordService => _documentRecordService.Value;
        public IDocumentSignatureService DocumentSignatureService => _documentSignatureService.Value;

        public INotificationService NotificationService => _notificationService.Value;
        public IEscalationLogService EscalationLogService => _escalationLogService.Value;

        public IAuditLogService AuditLogService => _auditLogService.Value;
        public IInventoryService InventoryService => _inventoryService.Value;

        public IJournalPostingService JournalPostingService => _journalPostingService.Value;

        public IDashboardService DashboardService => _dashboardService.Value;
    }
}

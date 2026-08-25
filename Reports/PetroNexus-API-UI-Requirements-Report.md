# PetroNexus API & UI Requirements Report

**Generated:** 31 July 2026  
**Scope:** 48 Controllers across 14 modules  
**Purpose:** Foundation for Frontend build prompt

---

## Table of Contents
1. [Finance Module](#1-finance-module)
2. [Procurement Module](#2-procurement-module)
3. [Logistics Module](#3-logistics-module)
4. [Fabrication & QA/QC Module](#4-fabrication--qaqc-module)
5. [Vendor Registration Module](#5-vendor-registration-module)
6. [Quotations Module](#6-quotations-module)
7. [Tenders Module](#7-tenders-module)
8. [Engineering Module](#8-engineering-module)
9. [CRM Module](#9-crm-module)
10. [Documents Module](#10-documents-module)
11. [Notifications Module](#11-notifications-module)
12. [System/Security Module](#12-systemsecurity-module)
13. [HR Module](#13-hr-module)
14. [Agency Module](#14-agency-module)
15. [Auth Module](#15-auth-module)
16. [Enum Color Mapping](#16-enum-color-mapping)
17. [Navigation Map](#17-navigation-map)
18. [Frontend Build Prompt Draft](#18-frontend-build-prompt-draft)

---

## 1. Finance Module

### 1.1 InvoicesController

#### Endpoints
| Method | Route | Auth | Description |
|---|---|---|---|
| GET | `/api/invoices` | ✅ | Paginated list of invoices |
| GET | `/api/invoices/{id}` | ✅ | Invoice details by ID |
| GET | `/api/invoices/lookup` | ✅ | Lightweight list for dropdowns |
| GET | `/api/invoices/summary` | ✅ | Summary list |
| POST | `/api/invoices` | ✅ | Create new invoice |
| PUT | `/api/invoices/{id}` | ✅ | Update invoice |
| DELETE | `/api/invoices/{id}` | ✅ | Delete invoice |
| POST | `/api/invoices/{id}/approve` | ✅ | Approve invoice + post journal entry |
| POST | `/api/invoices/{id}/cancel` | ✅ | Cancel invoice |
| POST | `/api/invoices/{id}/record-payment` | ✅ | Record payment against invoice |
| POST | `/api/invoices/generate/{tenderId}` | ✅ | Generate invoice from tender |

#### Response DTO: `InvoiceResponseDto`
| Field | Type | Enum/FK | UI Notes |
|---|---|---|---|
| Id | int | — | Hidden |
| InvoiceNumber | string | — | Text column |
| ClientId | int | FK→Client | Show ClientName instead |
| ClientName | string | — | Text column |
| TenderId | int? | FK→Tender | Show TenderTitle if present |
| TenderTitle | string? | — | Text column (optional) |
| IssueDate | DateTime | — | Date column |
| DueDate | DateTime? | — | Date column |
| Currency | CurrencyType | Enum | Badge (EGP=green, USD=blue, EUR=purple) |
| Status | InvoiceStatus | Enum | Status Badge (see Enum section) |
| RetentionPercentage | decimal | — | Percentage column |
| TotalAmount | decimal | — | Money column |
| TotalPaid | decimal | — | Money column |
| RemainingBalance | decimal | — | Money column (red if >0) |

#### Create DTO: `InvoiceCreateDto`
| Field | Type | Required | Validation | Input Type |
|---|---|---|---|---|
| InvoiceNumber | string | ✅ | — | text |
| ClientId | int | ✅ | >0 | dropdown (from /api/clients/lookup) |
| TenderId | int? | ❌ | — | dropdown (from /api/tenders/lookup) |
| IssueDate | DateTime | ✅ | — | date picker |
| DueDate | DateTime? | ❌ | — | date picker |
| Currency | CurrencyType | ✅ | IsInEnum | dropdown (EGP/USD/EUR) |
| Status | InvoiceStatus | ✅ | IsInEnum | dropdown |
| RetentionPercentage | decimal | ✅ | — | number (%) |

#### UI Translation
- **GET list** → DataGrid with columns: InvoiceNumber, ClientName, IssueDate, DueDate, Status(badge), TotalAmount, RemainingBalance. Search by invoice number. SortBy any column. Pagination (10/page).
- **POST** → "New Invoice" button (top-right of grid). Opens Modal with form fields above.
- **PUT** → Pencil icon per row → opens same form pre-filled.
- **DELETE** → Trash icon per row → Confirmation: "Delete invoice INV-2026-0001?"
- **Approve** → Green check button per row (only when Status=Draft). Confirmation: "Approve invoice INV-2026-0001? This will post a journal entry."
- **Cancel** → Red X button per row. Confirmation: "Cancel invoice INV-2026-0001?"
- **Record Payment** → Dollar icon per row → Opens Modal with: Amount (number), Method (dropdown: Cash/Bank/Transfer), ReferenceNumber (text).
- **Generate from Tender** → "Generate Invoice" button on Tender details page.

### 1.2 InvoiceItemsController
- **Sub-entity** → Displayed as a Tab inside Invoice details page, not in main navigation.
- CRUD: GET list, GET by id, POST, PUT, DELETE.
- **Response DTO fields**: Id, InvoiceId(FK), Description, Quantity, UnitPrice, TotalPrice.
- **Create form**: Description (text), Quantity (number), UnitPrice (number). TotalPrice auto-calculated.

### 1.3 PaymentsController
- **Endpoints**: Standard CRUD.
- **Response DTO**: Id, InvoiceId(FK→Invoice), Amount, Method, ReferenceNumber, PaymentDate, JournalEntryId.
- **UI**: Sub-tab inside Invoice details. Grid: Amount, Method, ReferenceNumber, PaymentDate.

### 1.4 AccountsController
- **Endpoints**: Standard CRUD + lookup.
- **Response DTO**: Id, Code, Name, Type(Enum: AccountType), ParentAccountId(FK→Account), ParentAccountName.
- **UI**: Tree-view grid (hierarchical by ParentAccount). Columns: Code, Name, Type(badge).
- **AccountType Badge**: Asset=blue, Liability=red, Equity=purple, Revenue=green, Expense=orange.

### 1.5 CostCentersController
- Standard CRUD + lookup.
- **Response DTO**: Id, Code, Name, TenderId(FK→Tender), TenderTitle, IsActive.
- **UI**: Grid with columns: Code, Name, TenderTitle, IsActive(toggle badge).

### 1.6 JournalEntriesController
- Standard CRUD + lookup.
- **Response DTO**: Id, ReferenceNumber, EntryDate, Description, Currency(Enum), SourceType, IsAutoGenerated, IsBalanced(bool), TotalDebitBase, TotalCreditBase.
- **UI**: Read-heavy grid. Auto-generated entries marked with badge. IsBalanced shown as green check / red X.
- **Note**: Manual creation should be restricted to Accountant role.

### 1.7 ExchangeRatesController
- Standard CRUD.
- **Response DTO**: Id, FromCurrency(Enum), ToCurrency(Enum), Rate, Date.
- **UI**: Grid: FromCurrency→ToCurrency, Rate, Date. Form: two dropdowns + number + date.

### 1.8 CompanySettingsController
- **Singleton** — No grid. Single form page.
- **GET** → loads single record. **PUT** → saves.
- **Form fields**: CompanyName(text), CompanyNameAr(text), LogoPath(file upload), TaxNumber(text), Address(textarea), Phone(text), Email(text), DefaultCurrency(dropdown).
- **Button**: "Save Settings" (single button, no add/delete).

---

## 2. Procurement Module

### 2.1 PurchaseOrdersController
#### Endpoints
| Method | Route | Auth | Description |
|---|---|---|---|
| GET | `/api/purchaseorders` | ✅ | Paginated list |
| GET | `/api/purchaseorders/{id}` | ✅ | Details |
| GET | `/api/purchaseorders/lookup` | ✅ | Dropdown list |
| GET | `/api/purchaseorders/summary` | ✅ | Summary |
| POST | `/api/purchaseorders` | ✅ | Create |
| PUT | `/api/purchaseorders/{id}` | ✅ | Update |
| DELETE | `/api/purchaseorders/{id}` | ✅ | Delete |

#### Response DTO: `PurchaseOrderResponseDto`
| Field | Type | Enum/FK | UI Notes |
|---|---|---|---|
| Id | int | — | Hidden |
| PoNumber | string | — | Text column |
| PrincipalId | int | FK→Principal | Show PrincipalName |
| PrincipalName | string | — | Text column |
| TenderId | int? | FK→Tender | Show TenderTitle |
| TenderTitle | string? | — | Text column |
| OrderDate | DateTime | — | Date column |
| RequiredDeliveryDate | DateTime? | — | Date column |
| Currency | CurrencyType | Enum | Badge |
| JournalEntryId | int? | — | Hidden (linked to journal entry) |
| LetterOfCreditId | int? | FK→LetterOfCredit | Show LC number if linked |

#### Create Form
| Field | Input Type |
|---|---|
| PoNumber | text (required) |
| PrincipalId | dropdown from /api/principals/lookup |
| TenderId | dropdown from /api/tenders/lookup (optional) |
| OrderDate | date picker |
| RequiredDeliveryDate | date picker (optional) |
| Currency | dropdown (EGP/USD/EUR) |
| LetterOfCreditId | dropdown from /api/lettersofcredit/lookup (optional) |

#### UI
- **Grid**: PoNumber, PrincipalName, TenderTitle, OrderDate, Currency(badge).
- **Details page**: Shows PO info + tabs for Items, Shipments, FabricationOrders.

### 2.2 PurchaseOrderItemsController
- **Sub-entity** inside PurchaseOrder details.
- **Response DTO**: Id, PurchaseOrderId(FK), Description, Quantity, UnitCost, UnitPrice, TotalCost, TotalPrice.
- **Form**: Description(text), Quantity(number), UnitCost(number), UnitPrice(number).

### 2.3 LettersOfCreditController
- Standard CRUD + lookup.
- **Response DTO**: Id, LcNumber, IssuingBank, Amount, Currency(Enum), IssueDate, ExpiryDate, IsSettled(bool), JournalEntryId.
- **Grid**: LcNumber, IssuingBank, Amount, Currency(badge), IssueDate, ExpiryDate, IsSettled(toggle).
- **Business Action**: `settle/{id}` → Blue button "Settle LC" when IsSettled=false. Confirmation: "Settle LC {LcNumber}? This will post a journal entry."

---

## 3. Logistics Module

### 3.1 ShipmentsController
#### Response DTO: `ShipmentResponseDto`
| Field | Type | Enum/FK | UI Notes |
|---|---|---|---|
| Id | int | — | Hidden |
| PurchaseOrderId | int | FK→PurchaseOrder | Show PoNumber |
| PoNumber | string | — | Text column |
| TrackingNumber | string? | — | Text column (link to tracking page) |
| Status | ShipmentStatus | Enum | Status Badge |
| PlannedDeliveryDate | DateTime? | — | Date column |
| ActualDeliveryDate | DateTime? | — | Date column |
| IsAtRiskOfDelay | bool | — | Red warning badge if true |
| ShippingCost | decimal? | — | Money column |
| CustomsCost | decimal? | — | Money column |

#### UI
- **Grid**: PoNumber, TrackingNumber, Status(badge), PlannedDeliveryDate, IsAtRisk(red flag if true).
- **Business Actions**:
  - `update-status/{id}` → Dropdown to change ShipmentStatus. Modal with status dropdown.
  - `mark-at-risk/{id}` → Orange warning button. Confirmation: "Mark shipment #{id} as at risk of delay?"

### 3.2 ShipmentTrackingEventsController
- **Sub-entity** inside Shipment details.
- **Response DTO**: Id, ShipmentId(FK), Description, EventDate, Location.
- **Form**: Description(textarea), EventDate(datetime picker), Location(text).

### 3.3 LiquidatedDamagesController
- Standard CRUD + lookup.
- **Response DTO**: Id, ShipmentId(FK), DaysDelayed, PenaltyPercentagePerDay, CalculatedAmount, Currency(Enum), Status(Enum: LdStatus), CalculatedAt, Notes.
- **Grid**: Shipment(PoNumber), DaysDelayed, CalculatedAmount, Status(badge).
- **LdStatus Badge**: Threatened=orange, Applied=red, Waived=green, Disputed=purple.

---

## 4. Fabrication & QA/QC Module

### 4.1 FabricationOrdersController
- Standard CRUD + lookup.
- **Response DTO**: Id, PurchaseOrderId(FK→PO), PoNumber, FabricatorName, FabricatorRating, PlannedStartDate, PlannedFinishDate, ActualStartDate, ActualFinishDate, CompletionPercentage, FabricationCost, JournalEntryId.
- **Grid**: PoNumber, FabricatorName, CompletionPercentage(progress bar), PlannedFinishDate.
- **Business Action**: `update-progress/{id}` → Modal with CompletionPercentage slider (0-100) + ActualStartDate/ActualFinishDate date pickers.

### 4.2 QualityInspectionsController
- Standard CRUD.
- **Response DTO**: Id, FabricationOrderId(FK), InspectionDate, InspectorName, Result(Enum: InspectionResult), Notes, PhotoPath.
- **Grid**: FabricationOrder, InspectorName, InspectionDate, Result(badge).
- **InspectionResult Badge**: Pending=gray, Passed=green, Failed=red, ConditionalPass=yellow.
- **Business Actions**: `approve/{id}` (green, when Result=Pending/ConditionalPass), `fail/{id}` (red, when Result=Pending).

### 4.3 NonConformanceReportsController
- Standard CRUD.
- **Response DTO**: Id, QualityInspectionId(FK), IssueDescription, CorrectiveAction, IsResolved(bool), ResolvedAt.
- **Grid**: QualityInspection, IssueDescription(truncated), IsResolved(badge).
- **Business Action**: `resolve/{id}` → Green button when IsResolved=false. Modal: CorrectiveAction(textarea).

---

## 5. Vendor Registration Module

### 5.1 VendorRegistrationsController
- Standard CRUD + lookup + summary.
- **Response DTO**: Id, ClientId(FK→Client), ClientName, RegistrationNumber, RegistrationDate, ExpiryDate, Status(Enum: RegistrationStatus).
- **Grid**: ClientName, RegistrationNumber, ExpiryDate, Status(badge).
- **RegistrationStatus Badge**: Active=green, PendingRenewal=yellow, Expired=red, Suspended=dark-gray.
- **Business Action**: `renew/{id}` → Blue "Renew" button when Status=PendingRenewal/Expired. Modal: NewExpiryDate(date picker).

### 5.2 RegistrationDocumentsController
- **Sub-entity** inside VendorRegistration details.
- **Response DTO**: Id, VendorRegistrationId(FK), DocumentName, FilePath, ExpiryDate.
- **Form**: DocumentName(text), FilePath(file upload), ExpiryDate(date picker, optional).

---

## 6. Quotations Module

### 6.1 QuotationsController
- Standard CRUD + lookup + summary.
- **Response DTO**: Id, TenderId(FK), TenderTitle, PrincipalId(FK), PrincipalName, SubmissionDate, TotalValue, Currency(Enum), Status(Enum: QuotationStatus), Notes.
- **Grid**: TenderTitle, PrincipalName, TotalValue, Currency(badge), Status(badge).
- **QuotationStatus Badge**: Draft=gray, SentToClient=blue, Accepted=green, Rejected=red, Expired=dark-gray.
- **Business Actions**:
  - `send-to-client/{id}` → Blue "Send" button (when Status=Draft). Confirmation: "Send quotation #{id} to client?"
  - `accept/{id}` → Green "Accept" button (when Status=SentToClient).
  - `reject/{id}` → Red "Reject" button (when Status=SentToClient). Modal: Reason(textarea).

### 6.2 QuotationItemsController
- **Sub-entity** inside Quotation details.
- **Response DTO**: Id, QuotationId(FK), Description, Quantity, UnitPrice, TotalPrice.
- **Form**: Description(text), Quantity(number), UnitPrice(number). TotalPrice auto-calc.

---

## 7. Tenders Module

### 7.1 TendersController
- Standard CRUD + lookup.
- **Response DTO**: Id, Title, ReferenceNumber, ClientId(FK), ClientName, AnnouncementDate, SubmissionDeadline, Status(Enum: TenderStatus), EstimatedValue, Currency(Enum), Notes, Source(Enum: TenderSource), TenderLeadId(FK?), CreatedAt.
- **Grid**: Title, ClientName, SubmissionDeadline, Status(badge), EstimatedValue.
- **TenderStatus Badge**: New=gray, UnderPreparation=yellow, Submitted=blue, Won=green, Lost=red, Cancelled=dark-gray.
- **TenderSource Badge**: Manual=gray, MinistryPortalScraper=blue, AiExtracted=purple.

### 7.2 TenderItemsController
- **Sub-entity** inside Tender details.
- **Response DTO**: Id, TenderId(FK), Description, Quantity, UnitOfMeasure, PrincipalProductId(FK?), PrincipalProductName.
- **Form**: Description(text), Quantity(number), UnitOfMeasure(text), PrincipalProductId(dropdown from /api/principalproducts/lookup, optional).

### 7.3 TenderLeadsController
- Standard CRUD + lookup.
- **Response DTO**: Id, Title, SourcePortalName, SourceUrl, DiscoveredAt, IsRelevantToCatalog, RelevanceScore.
- **Grid**: Title, SourcePortalName, DiscoveredAt, IsRelevant(badge), RelevanceScore(progress bar).
- **Business Action**: `convert-to-tender/{id}` → Green "Convert to Tender" button. Opens Modal with Tender create form pre-filled from Lead data.

### 7.4 TenderDocumentAnalysesController
- Standard CRUD.
- **Response DTO**: Id, TenderId(FK), SourceFilePath, ExtractedSpecifications, SuggestedPrincipalId(FK?), SuggestedPrincipalName, EstimatedMargin, AnalyzedAt.
- **UI**: Shown inside Tender details as a card/section. Display ExtractedSpecifications in a formatted text block.
- **Business Action**: `analyze/{tenderId}` → Purple "AI Analyze" button. Shows loading spinner during analysis.

---

## 8. Engineering Module

### 8.1 EngineeringProjectsController
- Standard CRUD + lookup.
- **Response DTO**: Id, TenderId(FK?), TenderTitle, Title, ScopeDescription, StartDate, EndDate.
- **Grid**: Title, TenderTitle, StartDate, EndDate.
- **Form**: Title(text), ScopeDescription(textarea), TenderId(dropdown, optional), StartDate(date), EndDate(date, optional).

### 8.2 EngineeringDeliverablesController
- **Sub-entity** inside EngineeringProject details.
- **Response DTO**: Id, EngineeringProjectId(FK), Name, Status(Enum: DeliverableStatus), SubmittedAt, ApprovedAt.
- **DeliverableStatus Badge**: Draft=gray, UnderReview=yellow, Approved=green, Rejected=red, Superseded=blue.
- **Business Actions**:
  - `submit/{id}` → Blue "Submit" button (when Status=Draft).
  - `approve/{id}` → Green "Approve" button (when Status=UnderReview).
  - `reject/{id}` → Red "Reject" button (when Status=UnderReview). Modal: Reason(textarea).

---

## 9. CRM Module

### 9.1 ClientsController
- Standard CRUD + lookup + summary.
- **Response DTO**: Id, Name, NameAr, Sector, Address, TaxNumber, CreatedAt.
- **Grid**: Name, Sector, TaxNumber, CreatedAt.
- **Form**: Name(text, required), NameAr(text, optional), Sector(text, required), Address(textarea), TaxNumber(text).
- **Details page**: Tabs for Contacts, Interactions, Tenders, PortalUsers.

### 9.2 ClientContactsController
- **Sub-entity** inside Client details.
- **Response DTO**: Id, ClientId(FK), FullName, JobTitle, Email, Phone, IsPrimary.
- **Grid**: FullName, JobTitle, Email, Phone, IsPrimary(star icon).
- **Form**: FullName(text, required), JobTitle(text), Email(email), Phone(text), IsPrimary(toggle).

### 9.3 ClientInteractionsController
- **Sub-entity** inside Client details.
- **Response DTO**: Id, ClientId(FK), ClientName, Type(Enum: InteractionType), InteractionDate, Notes, CreatedByUser, EmployeeName.
- **InteractionType Badge**: Call=blue, Meeting=green, Email=gray, Visit=purple.
- **Form**: Type(dropdown), InteractionDate(datetime), Notes(textarea).

### 9.4 ClientPortalUsersController
- **Sub-entity** inside Client details.
- **Response DTO**: Id, ClientId(FK), ClientName, AppUserId(FK→User), IsActive.
- **Grid**: AppUserId(→UserName), IsActive(toggle badge).
- **Form**: ClientId(dropdown), AppUserId(dropdown from /api/users/lookup), IsActive(toggle).
- **Business Actions**: `activate/{id}` (green), `deactivate/{id}` (red).

---

## 10. Documents Module

### 10.1 DocumentRecordsController
- Standard CRUD + lookup.
- **Response DTO**: Id, FileName, FilePath, Category(Enum: DocumentCategory), RelatedEntityType, RelatedEntityId, UploadedAt, UploadedByUserId.
- **DocumentCategory Badge**: TechnicalDatasheet=blue, QualityCertificate=green, Correspondence=gray, Contract=purple, Invoice=orange, Other=dark-gray.
- **File Upload**: `upload` endpoint → Drag & drop component + Browse button. Shows file name, size, upload progress.
- **Grid**: FileName, Category(badge), RelatedEntityType, UploadedAt, UploadedBy.

### 10.2 DocumentSignaturesController
- Standard CRUD.
- **Response DTO**: Id, DocumentRecordId(FK), SignerUserId(FK), SignerUserName, Status(Enum: SignatureStatus), SignatureImagePath, SignatureHash, RequestedAt, SignedAt, IpAddress.
- **SignatureStatus Badge**: Pending=yellow, Signed=green, Rejected=red, Expired=dark-gray.
- **Business Actions**:
  - `sign/{id}` → Green "Sign" button (when Status=Pending). May open signature pad component.
  - `reject/{id}` → Red "Reject" button (when Status=Pending). Modal: Reason(textarea).

---

## 11. Notifications Module

### 11.1 NotificationsController
- Standard CRUD + lookup.
- **Response DTO**: Id, Title, Message, Channel(Enum: NotificationChannel), RelatedEntityType, RelatedEntityId, TargetUserId, IsRead, CreatedAt.
- **NotificationChannel Badge**: InApp=blue, Email=gray, WhatsApp=green.
- **UI**: Bell icon in header with unread count badge. Dropdown panel showing recent notifications. IsRead shown as dot (unread=blue dot, read=no dot).
- **Business Action**: `mark-read/{id}` → Click on notification to mark as read.

### 11.2 EscalationLogsController
- Standard CRUD + lookup.
- **Response DTO**: Id, RelatedEntityType, RelatedEntityId, Level(Enum: EscalationLevel), Channel(Enum), Message, SentToUserId, SentAt, WasAcknowledged.
- **EscalationLevel Badge**: Info=blue, Warning=yellow, Critical=red.
- **Business Action**: `acknowledge/{id}` → Blue "Acknowledge" button (when WasAcknowledged=false).

---

## 12. System/Security Module

### 12.1 PermissionsController
- Standard CRUD + lookup.
- **Response DTO**: Id, Code, DescriptionAr, Module.
- **Grid**: Code, Module, DescriptionAr.
- **Form**: Code(text, required, unique), DescriptionAr(text), Module(text).

### 12.2 RolePermissionsController
- Standard CRUD.
- **Response DTO**: Id, RoleId(FK→Role), RoleName, PermissionId(FK→Permission), PermissionCode.
- **UI**: Matrix view — Roles as rows, Permissions as columns, checkboxes at intersections. Or master-detail: select Role → list of permissions with toggle switches.

### 12.3 AuditLogsController
- **Read-only** — GET + lookup only. No Create/Edit/Delete buttons.
- **Response DTO**: Id, EntityName, EntityId, Action(Enum: AuditAction), OldValues, NewValues, UserId, UserName, Timestamp.
- **AuditAction Badge**: Create=green, Update=blue, Delete=red.
- **Grid**: Timestamp, UserName, EntityName, Action(badge). Expandable row to show OldValues/NewValues as JSON diff.

### 12.4 CompanySettingsController
- See Finance Module §1.8 (singleton form).

---

## 13. HR Module

### 13.1 DepartmentsController
- Standard CRUD + lookup.
- **Response DTO**: Id, Name.
- **Grid**: Name only. Simple list.
- **Form**: Name(text, required).

### 13.2 EmployeesController
- Standard CRUD.
- **Response DTO**: Id, FullName, JobTitle, DepartmentId(FK), DepartmentName, AppUserId(FK?), HireDate, IsActive.
- **Grid**: FullName, JobTitle, DepartmentName, IsActive(badge).
- **Form**: FullName(text, required), JobTitle(text, required), DepartmentId(dropdown from /api/departments/lookup), HireDate(date), IsActive(toggle).
- **Details page**: Tabs for Salaries, Attendances, LeaveRequests, Documents.

### 13.3 EmployeeDocumentsController
- **Sub-entity** inside Employee details.
- **Response DTO**: Id, EmployeeId(FK), DocumentName, FilePath, UploadedAt.
- **Form**: DocumentName(text), FilePath(file upload).

### 13.4 AttendancesController
- Standard CRUD.
- **Response DTO**: Id, EmployeeId(FK), EmployeeName, Date, CheckIn(TimeSpan?), CheckOut(TimeSpan?), LateMinutes, IsAbsent.
- **Grid**: EmployeeName, Date, CheckIn, CheckOut, LateMinutes, IsAbsent(badge).
- **Form**: EmployeeId(dropdown), Date(date), CheckIn(time picker), CheckOut(time picker), IsAbsent(toggle).

### 13.5 LeaveRequestsController
- Standard CRUD.
- **Response DTO**: Id, EmployeeId(FK), EmployeeName, Type(Enum: LeaveType), StartDate, EndDate, Reason, IsApproved, ApprovedAt.
- **LeaveType Badge**: Annual=blue, Sick=red, Unpaid=gray, Other=purple.
- **Grid**: EmployeeName, Type(badge), StartDate→EndDate, IsApproved(badge).
- **Form**: EmployeeId(dropdown), Type(dropdown), StartDate(date), EndDate(date), Reason(textarea).

### 13.6 SalariesController
- Standard CRUD.
- **Response DTO**: Id, EmployeeId(FK), EmployeeName, BasicSalary, OvertimeAmount, Bonus, LateDeduction, AbsenceDeduction, NetSalary, Month, Year, Status(Enum: SalaryStatus), ApprovedAt, PaidAt.
- **SalaryStatus Badge**: Draft=gray, Approved=blue, Paid=green.
- **Grid**: EmployeeName, Month/Year, NetSalary, Status(badge).
- **Form**: EmployeeId(dropdown), BasicSalary(number), OvertimeAmount(number), Bonus(number), Month(1-12 dropdown), Year(number). NetSalary auto-calculated (Basic + Overtime + Bonus - Deductions).
- **Business Actions**: `approve/{id}` (blue, when Status=Draft → posts journal entry), `mark-paid/{id}` (green, when Status=Approved).

---

## 14. Agency Module

### 14.1 PrincipalsController
- Standard CRUD + lookup.
- **Response DTO**: Id, Name, Country, Website, AgencyAgreementPath, AgreementExpiryDate, PerformanceScore.
- **Grid**: Name, Country, AgreementExpiryDate, PerformanceScore(progress bar 0-100).
- **Form**: Name(text, required), Country(text), Website(url), AgencyAgreementPath(file upload), AgreementExpiryDate(date), PerformanceScore(number 0-100).
- **Details page**: Tabs for Contacts, Products, PerformanceReviews, Commissions.

### 14.2 PrincipalContactsController
- **Sub-entity** inside Principal details.
- **Response DTO**: Id, PrincipalId(FK), FullName, Email, Phone.
- **Form**: FullName(text, required), Email(email), Phone(text).

### 14.3 PrincipalProductsController
- **Sub-entity** inside Principal details.
- **Response DTO**: Id, PrincipalId(FK), PrincipalName, Name, PartNumber, Description, BasePrice, Currency(Enum).
- **Grid**: Name, PartNumber, BasePrice, Currency(badge).
- **Form**: Name(text, required), PartNumber(text), Description(textarea), BasePrice(number), Currency(dropdown).

### 14.4 PrincipalPerformanceReviewsController
- **Sub-entity** inside Principal details.
- **Response DTO**: Id, PrincipalId(FK), ReviewDate, QualityScore, DeliveryScore, SupportScore, OverallScore, Comments.
- **Grid**: ReviewDate, OverallScore(badge/progress), Comments(truncated).
- **Form**: ReviewDate(date), QualityScore(number 0-100), DeliveryScore(number), SupportScore(number), Comments(textarea). OverallScore auto-calc.

### 14.5 CommissionsController
- Standard CRUD.
- **Response DTO**: Id, PrincipalId(FK), PrincipalName, PurchaseOrderId(FK), PurchaseOrderNumber, CommissionPercentage, CommissionValue, Currency(Enum), DueDate, IsPaid, JournalEntryId.
- **Grid**: PrincipalName, PurchaseOrderNumber, CommissionValue, Currency(badge), IsPaid(badge).
- **Form**: PrincipalId(dropdown), PurchaseOrderId(dropdown), CommissionPercentage(number 0-100), CommissionValue(number), Currency(dropdown), DueDate(date).

---

## 15. Auth Module

### AuthController
| Method | Route | Auth | Description |
|---|---|---|---|
| POST | `/api/auth/login` | ❌ AllowAnonymous | Login with email+password |
| POST | `/api/auth/change-password` | ✅ | Change current user's password |
| POST | `/api/auth/forgot-password` | ❌ AllowAnonymous | Send reset link to email |
| POST | `/api/auth/reset-password` | ❌ AllowAnonymous | Reset password with token |
| POST | `/api/auth/logout` | ✅ | Logout current user |

#### UI
- **Login** → Full-page login form: Email(text), Password(password). "Forgot Password?" link. "Login" button (blue).
- **Change Password** → Settings page: CurrentPassword, NewPassword, ConfirmPassword. "Change" button.
- **Forgot Password** → Full-page: Email input. "Send Reset Link" button. Success message: "Check your email."
- **Reset Password** → Full-page (from email link): NewPassword, ConfirmPassword. "Reset" button.
- **Logout** → Dropdown menu item in header. Clears token, redirects to login.

### UsersController
| Method | Route | Auth | Role | Description |
|---|---|---|---|---|
| GET | `/api/users` | ✅ | Manager | List all users |
| GET | `/api/users/{numericId}` | ✅ | Manager | Get user by numeric ID |
| POST | `/api/users/create` | ✅ | Manager | Create new user |
| PUT | `/api/users/Update/{numericId}` | ✅ | Manager | Update user |
| DELETE | `/api/users/Delete/{numericId}` | ✅ | Manager | Delete user |
| PUT | `/api/users/profile` | ✅ | Manager,Hr,Accounting | Update own profile |
| POST | `/api/users/fix-numeric-ids` | ✅ | Manager | Fix missing numeric IDs |

#### UI
- **Users Grid** (Manager only): DisplayName, Email, Role, IsActive. 
- **Create User Form**: DisplayName, Email, Password, Role(dropdown), IsActive(toggle).
- **Profile Page**: Current user can edit own DisplayName.

---

## 16. Enum Color Mapping

### TenderStatus
| Value | Color | Hex |
|---|---|---|
| New | Gray | `#6b7280` |
| UnderPreparation | Yellow | `#f59e0b` |
| Submitted | Blue | `#3b82f6` |
| Won | Green | `#22c55e` |
| Lost | Red | `#ef4444` |
| Cancelled | Dark Gray | `#374151` |

### QuotationStatus
| Value | Color | Hex |
|---|---|---|
| Draft | Gray | `#6b7280` |
| SentToClient | Blue | `#3b82f6` |
| Accepted | Green | `#22c55e` |
| Rejected | Red | `#ef4444` |
| Expired | Dark Gray | `#374151` |

### InvoiceStatus
| Value | Color | Hex |
|---|---|---|
| Draft | Gray | `#6b7280` |
| Sent | Blue | `#3b82f6` |
| PartiallyPaid | Yellow | `#f59e0b` |
| Paid | Green | `#22c55e` |
| Overdue | Red | `#ef4444` |
| Cancelled | Dark Gray | `#374151` |
| Approved | Purple | `#8b5cf6` |

### ShipmentStatus
| Value | Color | Hex |
|---|---|---|
| AwaitingFabrication | Gray | `#6b7280` |
| InFabrication | Yellow | `#f59e0b` |
| ReadyForShipment | Blue | `#3b82f6` |
| Shipped | Indigo | `#6366f1` |
| InCustoms | Orange | `#f97316` |
| Delivered | Green | `#22c55e` |
| Delayed | Red | `#ef4444` |

### SalaryStatus
| Value | Color | Hex |
|---|---|---|
| Draft | Gray | `#6b7280` |
| Approved | Blue | `#3b82f6` |
| Paid | Green | `#22c55e` |

### RegistrationStatus
| Value | Color | Hex |
|---|---|---|
| Active | Green | `#22c55e` |
| PendingRenewal | Yellow | `#f59e0b` |
| Expired | Red | `#ef4444` |
| Suspended | Dark Gray | `#374151` |

### InspectionResult
| Value | Color | Hex |
|---|---|---|
| Pending | Gray | `#6b7280` |
| Passed | Green | `#22c55e` |
| Failed | Red | `#ef4444` |
| ConditionalPass | Yellow | `#f59e0b` |

### DeliverableStatus
| Value | Color | Hex |
|---|---|---|
| Draft | Gray | `#6b7280` |
| UnderReview | Yellow | `#f59e0b` |
| Approved | Green | `#22c55e` |
| Rejected | Red | `#ef4444` |
| Superseded | Blue | `#3b82f6` |

### SignatureStatus
| Value | Color | Hex |
|---|---|---|
| Pending | Yellow | `#f59e0b` |
| Signed | Green | `#22c55e` |
| Rejected | Red | `#ef4444` |
| Expired | Dark Gray | `#374151` |

### LdStatus
| Value | Color | Hex |
|---|---|---|
| Threatened | Orange | `#f97316` |
| Applied | Red | `#ef4444` |
| Waived | Green | `#22c55e` |
| Disputed | Purple | `#8b5cf6` |

### CurrencyType
| Value | Color | Hex |
|---|---|---|
| EGP | Green | `#22c55e` |
| USD | Blue | `#3b82f6` |
| EUR | Purple | `#8b5cf6` |

### AccountType
| Value | Color | Hex |
|---|---|---|
| Asset | Blue | `#3b82f6` |
| Liability | Red | `#ef4444` |
| Equity | Purple | `#8b5cf6` |
| Revenue | Green | `#22c55e` |
| Expense | Orange | `#f97316` |

### EscalationLevel
| Value | Color | Hex |
|---|---|---|
| Info | Blue | `#3b82f6` |
| Warning | Yellow | `#f59e0b` |
| Critical | Red | `#ef4444` |

### AuditAction
| Value | Color | Hex |
|---|---|---|
| Create | Green | `#22c55e` |
| Update | Blue | `#3b82f6` |
| Delete | Red | `#ef4444` |

### LeaveType
| Value | Color | Hex |
|---|---|---|
| Annual | Blue | `#3b82f6` |
| Sick | Red | `#ef4444` |
| Unpaid | Gray | `#6b7280` |
| Other | Purple | `#8b5cf6` |

### InteractionType
| Value | Color | Hex |
|---|---|---|
| Call | Blue | `#3b82f6` |
| Meeting | Green | `#22c55e` |
| Email | Gray | `#6b7280` |
| Visit | Purple | `#8b5cf6` |

### NotificationChannel
| Value | Color | Hex |
|---|---|---|
| InApp | Blue | `#3b82f6` |
| Email | Gray | `#6b7280` |
| WhatsApp | Green | `#22c55e` |

### TenderSource
| Value | Color | Hex |
|---|---|---|
| Manual | Gray | `#6b7280` |
| MinistryPortalScraper | Blue | `#3b82f6` |
| AiExtracted | Purple | `#8b5cf6` |

### DocumentCategory
| Value | Color | Hex |
|---|---|---|
| TechnicalDatasheet | Blue | `#3b82f6` |
| QualityCertificate | Green | `#22c55e` |
| Correspondence | Gray | `#6b7280` |
| Contract | Purple | `#8b5cf6` |
| Invoice | Orange | `#f97316` |
| Other | Dark Gray | `#374151` |

---

## 17. Navigation Map

### Sidebar Structure (Role-based)

```
📊 Dashboard (All roles)

📁 CRM
   ├─ Clients (Manager, Sales)
   ├─ Client Contacts (sub-page)
   ├─ Client Interactions (sub-page)
   └─ Client Portal Users (sub-page)

📋 Tenders & Quotations
   ├─ Tenders (Manager, Sales)
   ├─ Tender Leads (Manager, Sales)
   ├─ Tender Document Analyses (sub-page)
   ├─ Quotations (Manager, Sales)
   └─ Quotation Items (sub-page)

🏭 Agency
   ├─ Principals (Manager, Agency)
   ├─ Principal Contacts (sub-page)
   ├─ Principal Products (sub-page)
   ├─ Principal Performance Reviews (sub-page)
   └─ Commissions (Manager, Accounting)

🛒 Procurement & Logistics
   ├─ Purchase Orders (Manager, Procurement)
   ├─ Purchase Order Items (sub-page)
   ├─ Letters of Credit (Manager, Finance)
   ├─ Shipments (Manager, Logistics)
   ├─ Shipment Tracking Events (sub-page)
   ├─ Liquidated Damages (Manager, Logistics)
   ├─ Vendor Registrations (Manager, Procurement)
   └─ Registration Documents (sub-page)

🛠️ Fabrication & Engineering
   ├─ Fabrication Orders (Manager, Production)
   ├─ Quality Inspections (Manager, QA)
   ├─ Non-Conformance Reports (sub-page)
   ├─ Engineering Projects (Manager, Engineering)
   └─ Engineering Deliverables (sub-page)

💵 Finance
   ├─ Invoices (Manager, Accounting)
   ├─ Invoice Items (sub-page)
   ├─ Payments (sub-page)
   ├─ Accounts (Manager, Accounting)
   ├─ Cost Centers (Manager, Accounting)
   ├─ Journal Entries (Manager, Accounting)
   ├─ Exchange Rates (Manager, Accounting)
   └─ Company Settings (Manager only)

📄 Documents
   ├─ Document Records (All roles)
   └─ Document Signatures (sub-page)

👥 HR
   ├─ Departments (Manager, Hr)
   ├─ Employees (Manager, Hr)
   ├─ Employee Documents (sub-page)
   ├─ Attendances (Manager, Hr)
   ├─ Leave Requests (Manager, Hr)
   └─ Salaries (Manager, Hr, Accounting)

🔔 Notifications (All roles)
   ├─ Notifications
   └─ Escalation Logs (Manager)

⚙️ System/Admin (Manager only)
   ├─ Users
   ├─ Permissions
   ├─ Role Permissions
   └─ Audit Logs
```

---

## 18. Frontend Build Prompt Draft

```text
Build a React + TypeScript frontend for PetroNexus ERP, a comprehensive oil & gas 
agency management system. The backend API is at https://localhost:7026/api/ with 
JWT Bearer authentication.

TECH STACK:
- React 18 + TypeScript + Vite
- Tailwind CSS for styling
- React Router v6 for navigation
- Axios for API calls (with JWT interceptor)
- React Query (TanStack Query) for data fetching/caching
- Headless UI + Lucide React icons
- Recharts for dashboard charts

LAYOUT:
- Left sidebar (collapsible) with grouped navigation (see Navigation Map above)
- Top header: logo, global search, notifications bell (unread count), user dropdown (profile, logout)
- Main content area with breadcrumb + page content
- Responsive: sidebar collapses to hamburger on mobile

REUSABLE COMPONENTS (build once, use everywhere):
1. <DataGrid> — table with: search input, sortable columns, pagination (10/page), 
   row actions (edit/delete/custom buttons), status badges with colors from Enum mapping.
2. <EntityForm> — modal or page form with: auto-generated fields from DTO, 
   validation from FluentValidation rules, dropdowns from /lookup endpoints, 
   date pickers, toggles for booleans, file upload for path fields.
3. <StatusBadge> — colored badge for all Enums (see Enum Color Mapping section).
4. <ConfirmDialog> — confirmation modal with entity name in message.
5. <PageHeader> — title + breadcrumb + action buttons (New, Export).
6. <TabPanel> — for sub-entities inside parent details (e.g., Contacts tab inside Client).
7. <FileUpload> — drag & drop + browse, shows file name/size/progress.
8. <ProgressBar> — for CompletionPercentage, RelevanceScore, PerformanceScore.

COLOR SYSTEM (Tailwind):
- Primary: blue-600 (#2563eb) for main actions
- Success: green-500 (#22c55e) for approve/accept/paid
- Warning: yellow-500 (#f59e0b) for pending/under-preparation
- Danger: red-500 (#ef4444) for reject/delete/cancel/overdue
- Info: blue-500 (#3b82f6) for sent/submitted
- Neutral: gray-500 (#6b7280) for draft/new
- Purple: (#8b5cf6) for AI-generated/special

PAGES TO BUILD (48 controllers → ~25 main pages + sub-pages):
- Auth: Login, ForgotPassword, ResetPassword (full-page, no sidebar)
- Dashboard: KPI cards (active tenders, pending invoices, at-risk shipments, monthly revenue)
- CRM: Clients list+details (tabs: Contacts, Interactions, Tenders, PortalUsers)
- Tenders: Tenders list+details (tabs: Items, DocumentAnalysis, Quotations), 
  TenderLeads list (with "Convert to Tender" action)
- Quotations: list+details (tabs: Items), with Send/Accept/Reject actions
- Agency: Principals list+details (tabs: Contacts, Products, Reviews, Commissions)
- Procurement: PurchaseOrders list+details (tabs: Items, Shipments, Fabrication), 
  LettersOfCredit list (with Settle action)
- Logistics: Shipments list+details (tabs: TrackingEvents, LiquidatedDamages), 
  with UpdateStatus/MarkAtRisk actions
- Fabrication: FabricationOrders list+details (tabs: Inspections), 
  QualityInspections list (with Approve/Fail actions)
- Engineering: Projects list+details (tabs: Deliverables with Submit/Approve/Reject)
- Finance: Invoices list+details (tabs: Items, Payments, with Approve/Cancel/RecordPayment), 
  Accounts tree-view, JournalEntries list, ExchangeRates list, CompanySettings form (singleton)
- Documents: DocumentRecords list (with file upload), DocumentSignatures (with Sign/Reject)
- HR: Departments list, Employees list+details (tabs: Salaries, Attendance, Leave, Documents), 
  Salaries list (with Approve/MarkPaid), Attendances list, LeaveRequests list
- Notifications: bell dropdown + full page, EscalationLogs (with Acknowledge)
- System: Users list (Manager only), Permissions list, RolePermissions matrix, 
  AuditLogs read-only grid, CompanySettings form

API INTEGRATION:
- Base URL: from env variable VITE_API_URL
- JWT token stored in localStorage, attached as "Bearer {token}" in Authorization header
- 401 response → redirect to login page
- All list endpoints use: GET /api/{controller}?search=&pageIndex=1&pageSize=10&sortBy=&sortDesc=false
- All lookup endpoints: GET /api/{controller}/lookup → [{id, name}] for dropdowns
- API response format: { success: bool, message: string, data: T, errors?: string[] }
- Pagination response: { items: T[], totalCount, pageIndex, pageSize }

VALIDATION:
- Frontend validation mirrors FluentValidation rules from backend
- Required fields show red asterisk (*) 
- Email fields validate format
- Number fields validate min/max (e.g., CommissionPercentage 0-100)
- Date fields: EndDate must be >= StartDate (client-side check)
- Form submission shows validation errors inline under each field
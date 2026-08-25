using System;
using System.Linq.Expressions;
using Domain.Contracts;
using Domain.Models;
using Domain.Enums;

namespace Service.Specifications
{
    // ==========================================
    // EMPLOYEE SPECIFICATIONS
    // ==========================================
    public class EmployeeSearchSpecification : BaseSpecification<Employee, int>
    {
        public EmployeeSearchSpecification(string? search, int? departmentId)
            : base(x => (string.IsNullOrEmpty(search) || x.FullName.Contains(search) || x.JobTitle.Contains(search))
                        && (!departmentId.HasValue || x.DepartmentId == departmentId))
        {
            AddInclude(x => x.Department);
            SetTracking(false);
        }
    }

    public class EmployeeLookupSpecification : BaseSpecification<Employee, int>
    {
        public EmployeeLookupSpecification() : base(x => x.IsActive)
        {
            SetTracking(false);
        }
    }

    public class EmployeeDetailsSpecification : BaseSpecification<Employee, int>
    {
        public EmployeeDetailsSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.Department);
            AddInclude(x => x.Salaries);
            AddInclude(x => x.Attendances);
            AddInclude(x => x.LeaveRequests);
            AddInclude(x => x.Documents);
            SetTracking(false);
        }
    }

    public class EmployeeWithDepartmentSpecification : BaseSpecification<Employee, int>
    {
        public EmployeeWithDepartmentSpecification() : base()
        {
            AddInclude(x => x.Department);
            SetTracking(false);
        }
    }

    public class EmployeeWithManagerSpecification : BaseSpecification<Employee, int>
    {
        public EmployeeWithManagerSpecification() : base()
        {
            AddInclude(x => x.Department);
            SetTracking(false);
        }
    }

    // ==========================================
    // CLIENT (CUSTOMER) SPECIFICATIONS
    // ==========================================
    public class ClientSearchSpecification : BaseSpecification<Client, int>
    {
        public ClientSearchSpecification(string? search, string? sector)
            : base(x => (string.IsNullOrEmpty(search) || x.Name.Contains(search) || (x.NameAr != null && x.NameAr.Contains(search)))
                        && (string.IsNullOrEmpty(sector) || x.Sector == sector))
        {
            SetTracking(false);
        }
    }

    // ==========================================
    // PRINCIPAL (SUPPLIER) SPECIFICATIONS
    // ==========================================
    public class PrincipalSearchSpecification : BaseSpecification<Principal, int>
    {
        public PrincipalSearchSpecification(string? search, string? country)
            : base(x => (string.IsNullOrEmpty(search) || x.Name.Contains(search))
                        && (string.IsNullOrEmpty(country) || x.Country == country))
        {
            SetTracking(false);
        }
    }

    // ==========================================
    // INVENTORY SPECIFICATIONS
    // ==========================================
    public class InventorySearchSpecification : BaseSpecification<FabricationOrder, int>
    {
        public InventorySearchSpecification(string? fabricatorName)
            : base(x => string.IsNullOrEmpty(fabricatorName) || x.FabricatorName.Contains(fabricatorName))
        {
            AddInclude(x => x.PurchaseOrder);
            SetTracking(false);
        }
    }

    public class WarehouseInventorySpecification : BaseSpecification<Shipment, int>
    {
        public WarehouseInventorySpecification(ShipmentStatus? status)
            : base(x => !status.HasValue || x.Status == status)
        {
            AddInclude(x => x.PurchaseOrder);
            SetTracking(false);
        }
    }

    // ==========================================
    // INVOICE (SALES) SPECIFICATIONS
    // ==========================================
    public class SalesInvoiceSpecification : BaseSpecification<Invoice, int>
    {
        public SalesInvoiceSpecification(string? invoiceNumber, InvoiceStatus? status)
            : base(x => (string.IsNullOrEmpty(invoiceNumber) || x.InvoiceNumber.Contains(invoiceNumber))
                        && (!status.HasValue || x.Status == status))
        {
            AddInclude(x => x.Client);
            AddInclude(x => x.Tender);
            SetTracking(false);
        }
    }

    // ==========================================
    // PURCHASE ORDER SPECIFICATIONS
    // ==========================================
    public class PurchaseInvoiceSpecification : BaseSpecification<PurchaseOrder, int>
    {
        public PurchaseInvoiceSpecification(string? poNumber)
            : base(x => string.IsNullOrEmpty(poNumber) || x.PoNumber.Contains(poNumber))
        {
            AddInclude(x => x.Principal);
            AddInclude(x => x.Tender);
            SetTracking(false);
        }
    }

    // ==========================================
    // JOURNAL ENTRY SPECIFICATIONS
    // ==========================================
    public class JournalEntrySpecification : BaseSpecification<JournalEntry, int>
    {
        public JournalEntrySpecification(string? refNumber, DateTime? fromDate, DateTime? toDate)
            : base(x => (string.IsNullOrEmpty(refNumber) || x.ReferenceNumber.Contains(refNumber))
                        && (!fromDate.HasValue || x.EntryDate >= fromDate.Value)
                        && (!toDate.HasValue || x.EntryDate <= toDate.Value))
        {
            AddInclude(x => x.Lines);
            SetTracking(false);
        }
    }

    // ==========================================
    // TRIAL BALANCE SPECIFICATIONS
    // ==========================================
    public class TrialBalanceSpecification : BaseSpecification<JournalEntryLine, int>
    {
        public TrialBalanceSpecification(DateTime? fromDate, DateTime? toDate)
            : base(x => (!fromDate.HasValue || x.JournalEntry.EntryDate >= fromDate.Value)
                        && (!toDate.HasValue || x.JournalEntry.EntryDate <= toDate.Value))
        {
            AddInclude(x => x.Account);
            AddInclude(x => x.JournalEntry);
            SetTracking(false);
        }
    }

    // ==========================================
    // GENERAL LEDGER SPECIFICATIONS
    // ==========================================
    public class GeneralLedgerSpecification : BaseSpecification<JournalEntryLine, int>
    {
        public GeneralLedgerSpecification(int accountId, DateTime? fromDate, DateTime? toDate)
            : base(x => x.AccountId == accountId
                        && (!fromDate.HasValue || x.JournalEntry.EntryDate >= fromDate.Value)
                        && (!toDate.HasValue || x.JournalEntry.EntryDate <= toDate.Value))
        {
            AddInclude(x => x.Account);
            AddInclude(x => x.JournalEntry);
            SetTracking(false);
        }
    }

    // ==========================================
    // CASH FLOW SPECIFICATIONS
    // ==========================================
    public class CashFlowSpecification : BaseSpecification<JournalEntryLine, int>
    {
        public CashFlowSpecification(DateTime? fromDate, DateTime? toDate)
            : base(x => (x.Account.Type == AccountType.Asset) // simplifed filter for cash/bank accounts
                        && (!fromDate.HasValue || x.JournalEntry.EntryDate >= fromDate.Value)
                        && (!toDate.HasValue || x.JournalEntry.EntryDate <= toDate.Value))
        {
            AddInclude(x => x.Account);
            AddInclude(x => x.JournalEntry);
            SetTracking(false);
        }
    }

    // ==========================================
    // PARTNER LEDGER SPECIFICATIONS
    // ==========================================
    public class PartnerLedgerSpecification : BaseSpecification<JournalEntryLine, int>
    {
        public PartnerLedgerSpecification(int? accountId)
            : base(x => !accountId.HasValue || x.AccountId == accountId)
        {
            AddInclude(x => x.Account);
            AddInclude(x => x.JournalEntry);
            SetTracking(false);
        }
    }

    // ==========================================
    // PROJECT LEDGER SPECIFICATIONS
    // ==========================================
    public class ProjectLedgerSpecification : BaseSpecification<JournalEntryLine, int>
    {
        public ProjectLedgerSpecification(int costCenterId)
            : base(x => x.CostCenterId == costCenterId)
        {
            AddInclude(x => x.CostCenter);
            AddInclude(x => x.JournalEntry);
            SetTracking(false);
        }
    }

    // ==========================================
    // BANK TRANSACTION SPECIFICATIONS
    // ==========================================
    public class BankTransactionSpecification : BaseSpecification<JournalEntryLine, int>
    {
        public BankTransactionSpecification(int bankAccountId)
            : base(x => x.AccountId == bankAccountId)
        {
            AddInclude(x => x.JournalEntry);
            SetTracking(false);
        }
    }

    // ==========================================
    // TREASURY TRANSACTION SPECIFICATIONS
    // ==========================================
    public class TreasuryTransactionSpecification : BaseSpecification<JournalEntryLine, int>
    {
        public TreasuryTransactionSpecification(int treasuryAccountId)
            : base(x => x.AccountId == treasuryAccountId)
        {
            AddInclude(x => x.JournalEntry);
            SetTracking(false);
        }
    }

    // ==========================================
    // ATTENDANCE SPECIFICATIONS
    // ==========================================
    public class AttendanceSpecification : BaseSpecification<Attendance, int>
    {
        public AttendanceSpecification(int? employeeId, DateTime? date)
            : base(x => (!employeeId.HasValue || x.EmployeeId == employeeId.Value)
                        && (!date.HasValue || x.Date.Date == date.Value.Date))
        {
            AddInclude(x => x.Employee);
            SetTracking(false);
        }
    }

    // ==========================================
    // SALARY SPECIFICATIONS
    // ==========================================
    public class SalarySpecification : BaseSpecification<Salary, int>
    {
        public SalarySpecification(int? employeeId, int? month, int? year)
            : base(x => (!employeeId.HasValue || x.EmployeeId == employeeId.Value)
                        && (!month.HasValue || x.Month == month.Value)
                        && (!year.HasValue || x.Year == year.Value))
        {
            AddInclude(x => x.Employee);
            SetTracking(false);
        }
    }

    // ==========================================
    // NOTIFICATION SPECIFICATIONS
    // ==========================================
    public class NotificationSpecification : BaseSpecification<Notification, int>
    {
        public NotificationSpecification(string? userId, bool? isRead)
            : base(x => (string.IsNullOrEmpty(userId) || x.TargetUserId == userId)
                        && (!isRead.HasValue || x.IsRead == isRead.Value))
        {
            SetTracking(false);
        }
    }

    // ==========================================
    // ROLE PERMISSION SPECIFICATIONS
    // ==========================================
    public class RolePermissionSpecification : BaseSpecification<RolePermission, int>
    {
        public RolePermissionSpecification(string? roleId)
            : base(x => string.IsNullOrEmpty(roleId) || x.RoleId == roleId)
        {
            AddInclude(x => x.Role);
            AddInclude(x => x.Permission);
            SetTracking(false);
        }
    }
}

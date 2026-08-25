using System.Linq;
using Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Data
{
    public class PetroNexusDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public PetroNexusDbContext(
            DbContextOptions<PetroNexusDbContext> options
        ) : base(options)
        {
        }

        // =========================
        // 📄 المناقصات وCRM
        // =========================
        public DbSet<Client> Clients { get; set; }
        public DbSet<ClientContact> ClientContacts { get; set; }
        public DbSet<ClientInteraction> ClientInteractions { get; set; }
        public DbSet<ClientPortalUser> ClientPortalUsers { get; set; }
        public DbSet<Tender> Tenders { get; set; }
        public DbSet<TenderItem> TenderItems { get; set; }
        public DbSet<TenderLead> TenderLeads { get; set; }
        public DbSet<TenderDocumentAnalysis> TenderDocumentAnalyses { get; set; }
        public DbSet<Quotation> Quotations { get; set; }
        public DbSet<QuotationItem> QuotationItems { get; set; }

        // =========================
        // 🏭 الوكالة (Principals)
        // =========================
        public DbSet<Principal> Principals { get; set; }
        public DbSet<PrincipalProduct> PrincipalProducts { get; set; }
        public DbSet<PrincipalContact> PrincipalContacts { get; set; }
        public DbSet<PrincipalPerformanceReview> PrincipalPerformanceReviews { get; set; }
        public DbSet<Commission> Commissions { get; set; }

        // =========================
        // 🏷️ تسجيل وتأهيل الموردين
        // =========================
        public DbSet<VendorRegistration> VendorRegistrations { get; set; }
        public DbSet<RegistrationDocument> RegistrationDocuments { get; set; }

        // =========================
        // 🚢 الاستيراد والشحن
        // =========================
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<PurchaseOrderItem> PurchaseOrderItems { get; set; }
        public DbSet<LetterOfCredit> LettersOfCredit { get; set; }
        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<ShipmentTrackingEvent> ShipmentTrackingEvents { get; set; }
        public DbSet<LiquidatedDamage> LiquidatedDamages { get; set; }

        // =========================
        // 🛠️ التصنيع المحلي والجودة
        // =========================
        public DbSet<FabricationOrder> FabricationOrders { get; set; }
        public DbSet<QualityInspection> QualityInspections { get; set; }
        public DbSet<NonConformanceReport> NonConformanceReports { get; set; }

        // =========================
        // 📐 الخدمات الهندسية
        // =========================
        public DbSet<EngineeringProject> EngineeringProjects { get; set; }
        public DbSet<EngineeringDeliverable> EngineeringDeliverables { get; set; }

        // =========================
        // 💵 المالية
        // =========================
        public DbSet<Account> Accounts { get; set; }
        public DbSet<CostCenter> CostCenters { get; set; }
        public DbSet<JournalEntry> JournalEntries { get; set; }
        public DbSet<JournalEntryLine> JournalEntryLines { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceItem> InvoiceItems { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<ExchangeRate> ExchangeRates { get; set; }
        public DbSet<CompanySettings> CompanySettings { get; set; }

        // =========================
        // 📁 المستندات والتوقيع
        // =========================
        public DbSet<DocumentRecord> DocumentRecords { get; set; }
        public DbSet<DocumentSignature> DocumentSignatures { get; set; }

        // =========================
        // 👥 الموارد البشرية
        // =========================
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeDocument> EmployeeDocuments { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }
        public DbSet<Salary> Salaries { get; set; }

        // =========================
        // 🔐 الأمان والصلاحيات
        // =========================
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        // =========================
        // 🔔 التنبيهات والتدقيق
        // =========================
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<EscalationLog> EscalationLogs { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // =====================================================================
            // 1) الافتراضي: كل الـ Foreign Keys بقيمة Restrict
            //    عشان نتفادى خطأ SQL Server الشهير "multiple cascade paths" اللي
            //    هيحصل أكيد في Graph بالحجم ده لو سبنا الـ Cascade الافتراضي شغال.
            //    لازم يتنفذ الأول قبل أي تعديل يدوي تحت، عشان اللي بعده يقدر يجاوزه.
            // =====================================================================
            foreach (var fk in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }

            // =====================================================================
            // 2) دقة افتراضية لكل خانة decimal (المبالغ المالية) — 18 رقم، 4 عشرية
            // =====================================================================
            foreach (var property in builder.Model.GetEntityTypes()
                         .SelectMany(t => t.GetProperties())
                         .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetPrecision(18);
                property.SetScale(4);
            }

            // =====================================================================
            // 3) تطبيق كل كلاسات IEntityTypeConfiguration<T> اللي في
            //    Infrastructure/Persistence/Configurations — كل واحد منهم بيعرّف
            //    علاقاته وindexes بتاعته بنفسه، وأي .OnDelete() يكتبه هنا
            //    بيجاوز الـ Restrict الافتراضي اللي اتحط فوق في الخطوة (1)
            // =====================================================================
            builder.ApplyConfigurationsFromAssembly(typeof(PetroNexusDbContext).Assembly);
        }
    }
}

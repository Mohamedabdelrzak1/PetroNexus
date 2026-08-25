-- =========================================================
-- 🌱 Seed Chart of Accounts (PetroNexus)
-- =========================================================
-- This script seeds the minimum required accounts that
-- IJournalPostingService expects. It is idempotent — safe to
-- run multiple times (only inserts missing accounts).
--
-- Account codes are taken EXACTLY from JournalPostingService.cs:
--   1101 Cash/Bank, 1102 LC Margin, 1201 AR, 1301 Inventory,
--   1302 WIP, 2101 AP, 2102 Salary Payable, 4101 Revenue,
--   4201 Commission Revenue, 5101 Salary Expense,
--   5102 Shipping Expense, 5103 Fabrication Expense, 5104 COGS
-- =========================================================

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Accounts')
BEGIN
    PRINT 'Accounts table does not exist. Run EF Core migrations first.';
    RETURN;
END

-- ─── Assets ────────────────────────────────────────────────
IF NOT EXISTS (SELECT 1 FROM Accounts WHERE Code = '1101')
    INSERT INTO Accounts (Code, Name, Type, ParentAccountId) VALUES ('1101', N'نقدية / بنك', 1, NULL);

IF NOT EXISTS (SELECT 1 FROM Accounts WHERE Code = '1102')
    INSERT INTO Accounts (Code, Name, Type, ParentAccountId) VALUES ('1102', N'وديعة هامش خطاب الاعتماد', 1, NULL);

IF NOT EXISTS (SELECT 1 FROM Accounts WHERE Code = '1201')
    INSERT INTO Accounts (Code, Name, Type, ParentAccountId) VALUES ('1201', N'عملاء (مدينون)', 1, NULL);

IF NOT EXISTS (SELECT 1 FROM Accounts WHERE Code = '1301')
    INSERT INTO Accounts (Code, Name, Type, ParentAccountId) VALUES ('1301', N'مخزون', 1, NULL);

IF NOT EXISTS (SELECT 1 FROM Accounts WHERE Code = '1302')
    INSERT INTO Accounts (Code, Name, Type, ParentAccountId) VALUES ('1302', N'مخزون تحت التصنيع (WIP)', 1, NULL);

-- ─── Liabilities ───────────────────────────────────────────
IF NOT EXISTS (SELECT 1 FROM Accounts WHERE Code = '2101')
    INSERT INTO Accounts (Code, Name, Type, ParentAccountId) VALUES ('2101', N'موردون (دائنون)', 2, NULL);

IF NOT EXISTS (SELECT 1 FROM Accounts WHERE Code = '2102')
    INSERT INTO Accounts (Code, Name, Type, ParentAccountId) VALUES ('2102', N'رواتب مستحقة', 2, NULL);

-- ─── Revenue ───────────────────────────────────────────────
IF NOT EXISTS (SELECT 1 FROM Accounts WHERE Code = '4101')
    INSERT INTO Accounts (Code, Name, Type, ParentAccountId) VALUES ('4101', N'إيرادات', 4, NULL);

IF NOT EXISTS (SELECT 1 FROM Accounts WHERE Code = '4201')
    INSERT INTO Accounts (Code, Name, Type, ParentAccountId) VALUES ('4201', N'إيرادات عمولات', 4, NULL);

-- ─── Expenses ──────────────────────────────────────────────
IF NOT EXISTS (SELECT 1 FROM Accounts WHERE Code = '5101')
    INSERT INTO Accounts (Code, Name, Type, ParentAccountId) VALUES ('5101', N'مصروفات رواتب', 5, NULL);

IF NOT EXISTS (SELECT 1 FROM Accounts WHERE Code = '5102')
    INSERT INTO Accounts (Code, Name, Type, ParentAccountId) VALUES ('5102', N'مصروفات شحن', 5, NULL);

IF NOT EXISTS (SELECT 1 FROM Accounts WHERE Code = '5103')
    INSERT INTO Accounts (Code, Name, Type, ParentAccountId) VALUES ('5103', N'مصروفات تصنيع', 5, NULL);

IF NOT EXISTS (SELECT 1 FROM Accounts WHERE Code = '5104')
    INSERT INTO Accounts (Code, Name, Type, ParentAccountId) VALUES ('5104', N'تكلفة البضاعة المباعة (COGS)', 5, NULL);

PRINT 'Chart of Accounts seeded successfully.';
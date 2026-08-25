# PetroNexus — دليل ربط الـ API الجديد بالفرونت إند

> هذا الملف يحتوي على كل الـ Endpoints الجديدة اللي اتضافت للمشروع،
> مع أمثلة Request/Response كاملة لكل endpoint، خاصة الـ Dashboard endpoints.

---

## 📊 1) Dashboard Endpoints (الأهم — `/api/Dashboard`)

كل الـ Dashboard endpoints دي محمية بـ `[Authorize]` وبترد بنفس شكل `ApiResponse<T>` الموحد.

### 1.1) `GET /api/Dashboard/tender-win-rate`

نسبة فوز المناقصات إجمالاً + تقسيمها حسب العميل والقطاع + عدد الـ Leads الجديدة هذا الأسبوع.

**Response Example:**
```json
{
  "success": true,
  "message": "Retrieved successfully.",
  "data": {
    "totalTenders": 20,
    "won": 8,
    "lost": 4,
    "inProgress": 8,
    "winRatePercentage": 40.0,
    "newLeadsThisWeek": 4,
    "byClient": [
      {
        "clientName": "EGPC",
        "total": 10,
        "won": 5,
        "winRate": 50.0
      },
      {
        "clientName": "Petrobel",
        "total": 6,
        "won": 2,
        "winRate": 33.33
      },
      {
        "clientName": "ENPPI",
        "total": 4,
        "won": 1,
        "winRate": 25.0
      }
    ],
    "bySector": [
      {
        "sector": "Oil & Gas",
        "total": 14,
        "won": 6,
        "winRate": 42.86
      },
      {
        "sector": "Petrochemical",
        "total": 4,
        "won": 1,
        "winRate": 25.0
      },
      {
        "sector": "Power",
        "total": 2,
        "won": 1,
        "winRate": 50.0
      }
    ]
  },
  "errors": null
}
```

---

### 1.2) `GET /api/Dashboard/expediting-overview`

لوحة التعجيل الشاملة — كل الشحنات الجارية مع حالتها، المخاطر، والغرامات.

**Response Example:**
```json
{
  "success": true,
  "message": "Retrieved successfully.",
  "data": {
    "shipmentsByStatus": {
      "AwaitingFabrication": 3,
      "InFabrication": 5,
      "ReadyForShipment": 2,
      "Shipped": 4,
      "InCustoms": 1,
      "Delayed": 2
    },
    "atRiskShipments": [
      {
        "id": 15,
        "purchaseOrderId": 8,
        "poNumber": "PO-2026-0008",
        "trackingNumber": "TRK-789456",
        "status": "Delayed",
        "plannedDeliveryDate": "2026-08-05T00:00:00Z",
        "isAtRiskOfDelay": true,
        "daysUntilDeadline": 4,
        "liquidatedDamages": [
          {
            "id": 3,
            "daysDelayed": 2,
            "calculatedAmount": 5000.00,
            "currency": "USD",
            "status": "Threatened"
          }
        ]
      },
      {
        "id": 12,
        "purchaseOrderId": 5,
        "poNumber": "PO-2026-0005",
        "trackingNumber": "TRK-123456",
        "status": "Shipped",
        "plannedDeliveryDate": "2026-08-10T00:00:00Z",
        "isAtRiskOfDelay": true,
        "daysUntilDeadline": 9,
        "liquidatedDamages": []
      }
    ],
    "activeShipments": [
      {
        "id": 15,
        "purchaseOrderId": 8,
        "poNumber": "PO-2026-0008",
        "trackingNumber": "TRK-789456",
        "status": "Delayed",
        "plannedDeliveryDate": "2026-08-05T00:00:00Z",
        "isAtRiskOfDelay": true,
        "liquidatedDamages": [
          {
            "id": 3,
            "daysDelayed": 2,
            "calculatedAmount": 5000.00,
            "currency": "USD",
            "status": "Threatened"
          }
        ]
      },
      {
        "id": 12,
        "purchaseOrderId": 5,
        "poNumber": "PO-2026-0005",
        "trackingNumber": "TRK-123456",
        "status": "Shipped",
        "plannedDeliveryDate": "2026-08-10T00:00:00Z",
        "isAtRiskOfDelay": true,
        "liquidatedDamages": []
      },
      {
        "id": 20,
        "purchaseOrderId": 10,
        "poNumber": "PO-2026-0010",
        "trackingNumber": null,
        "status": "InFabrication",
        "plannedDeliveryDate": "2026-09-15T00:00:00Z",
        "isAtRiskOfDelay": false,
        "liquidatedDamages": []
      }
    ]
  },
  "errors": null
}
```

---

### 1.3) `GET /api/Dashboard/principal-profitability`

ربحية كل Principal: قيمة أوامر الشراء، العمولات، متوسط الأداء، عدد المشاكل (NCRs).

**Response Example:**
```json
{
  "success": true,
  "message": "Retrieved successfully.",
  "data": {
    "items": [
      {
        "principalId": 1,
        "principalName": "Alfa Laval",
        "totalPurchaseOrderValue": 1250000.00,
        "totalCommissions": 62500.00,
        "averagePerformanceScore": 4.2,
        "nonConformanceCount": 2
      },
      {
        "principalId": 2,
        "principalName": "Schneider Electric",
        "totalPurchaseOrderValue": 850000.00,
        "totalCommissions": 42500.00,
        "averagePerformanceScore": 3.8,
        "nonConformanceCount": 0
      },
      {
        "principalId": 3,
        "principalName": "ABB",
        "totalPurchaseOrderValue": 420000.00,
        "totalCommissions": 21000.00,
        "averagePerformanceScore": 4.5,
        "nonConformanceCount": 1
      }
    ]
  },
  "errors": null
}
```

---

### 1.4) `GET /api/Dashboard/revenue-forecast`

توقع الإيرادات بناءً على المناقصات الجارية × معامل احتمالية.

> **معاملات الاحتمالية (تقدير مبدئي):**
> - `Submitted` = 50% فرصة الفوز
> - `UnderPreparation` = 20% فرصة الفوز

**Response Example:**
```json
{
  "success": true,
  "message": "Retrieved successfully.",
  "data": {
    "totalForecast": 750000.00,
    "submittedValue": 1000000.00,
    "underPreparationValue": 1250000.00,
    "items": [
      {
        "tenderId": 5,
        "title": "Supply of Heat Exchangers — EGPC",
        "status": "Submitted",
        "estimatedValue": 600000.00,
        "probabilityFactor": 0.50,
        "expectedValue": 300000.00
      },
      {
        "tenderId": 8,
        "title": "Skid Package — Petrobel",
        "status": "Submitted",
        "estimatedValue": 400000.00,
        "probabilityFactor": 0.50,
        "expectedValue": 200000.00
      },
      {
        "tenderId": 12,
        "title": "Valves Supply — ENPPI",
        "status": "UnderPreparation",
        "estimatedValue": 500000.00,
        "probabilityFactor": 0.20,
        "expectedValue": 100000.00
      },
      {
        "tenderId": 15,
        "title": "Pipes — TAQA",
        "status": "UnderPreparation",
        "estimatedValue": 750000.00,
        "probabilityFactor": 0.20,
        "expectedValue": 150000.00
      }
    ]
  },
  "errors": null
}
```

---

### 1.5) `GET /api/Dashboard/financial-summary`

إجمالي الفواتير (مُصدرة، مدفوعة، متبقية) + رصيد كل عملة على حدة.

**Response Example:**
```json
{
  "success": true,
  "message": "Retrieved successfully.",
  "data": {
    "totalInvoiced": 2150000.00,
    "totalPaid": 1620000.00,
    "totalOutstanding": 530000.00,
    "balancesByCurrency": [
      {
        "currency": "EGP",
        "invoiced": 500000.00,
        "paid": 450000.00,
        "outstanding": 50000.00
      },
      {
        "currency": "USD",
        "invoiced": 1500000.00,
        "paid": 1100000.00,
        "outstanding": 400000.00
      },
      {
        "currency": "EUR",
        "invoiced": 150000.00,
        "paid": 70000.00,
        "outstanding": 80000.00
      }
    ]
  },
  "errors": null
}
```

---

### 1.6) `GET /api/Dashboard/revenue-collection-monthly`

بيانات شهرية (آخر 7 شهور) لعمل Bar Chart "المفوتر مقابل المحصّل".

**Response Example:**
```json
{
  "success": true,
  "message": "Retrieved successfully.",
  "data": [
    { "month": "Feb", "invoiced": 62000.00, "collected": 54000.00 },
    { "month": "Mar", "invoiced": 71000.00, "collected": 68000.00 },
    { "month": "Apr", "invoiced": 85000.00, "collected": 79000.00 },
    { "month": "May", "invoiced": 93000.00, "collected": 88000.00 },
    { "month": "Jun", "invoiced": 78000.00, "collected": 72000.00 },
    { "month": "Jul", "invoiced": 105000.00, "collected": 95000.00 },
    { "month": "Aug", "invoiced": 120000.00, "collected": 45000.00 }
  ],
  "errors": null
}
```

---

### 1.7) `GET /api/Dashboard/tender-pipeline`

توزيع المناقصات النشطة على مراحلها لعمل Pie/Donut Chart.

> **ملاحظة:** اسم المرحلة `UnderPrep` في الرد (مش `UnderPreparation`) عشان متطابق مع الفرونت.

**Response Example:**
```json
{
  "success": true,
  "message": "Retrieved successfully.",
  "data": [
    { "stage": "Won", "count": 8 },
    { "stage": "Submitted", "count": 5 },
    { "stage": "UnderPrep", "count": 3 },
    { "stage": "Lost", "count": 4 }
  ],
  "errors": null
}
```

---

## 🔧 2) Endpoints الأكشن الناقصة (على Controllers موجودة)

### 2.1) `POST /api/TenderLeads/{id}/convert-to-tender`

حوّل TenderLead لـ Tender رسمي.

**Request:**
```
POST /api/TenderLeads/5/convert-to-tender
```
(Body فارغ — مش محتاج أي بيانات)

**Response:**
```json
{
  "success": true,
  "message": "TenderLead converted to Tender successfully.",
  "data": {
    "id": 25,
    "title": "Supply of Industrial Valves",
    "referenceNumber": null,
    "clientId": 0,
    "clientName": null,
    "announcementDate": "2026-07-28T10:00:00Z",
    "submissionDeadline": "2026-08-27T10:00:00Z",
    "status": "New",
    "estimatedValue": null,
    "currency": "EGP",
    "notes": "Converted from TenderLead #5 — Source: بوابة المشتريات الحكومية",
    "source": "Manual",
    "tenderLeadId": 5,
    "createdAt": "2026-08-01T16:20:00Z"
  },
  "errors": null
}
```

---

### 2.2) `POST /api/EscalationLogs/{id}/acknowledge`

حدّث `WasAcknowledged = true`.

**Request:**
```
POST /api/EscalationLogs/12/acknowledge
```
(Body فارغ)

**Response:**
```json
{
  "success": true,
  "message": "Escalation acknowledged successfully.",
  "data": null,
  "errors": null
}
```

---

### 2.3) `POST /api/Notifications/{id}/mark-read`

حدّث `IsRead = true`.

**Request:**
```
POST /api/Notifications/45/mark-read
```
(Body فارغ)

**Response:**
```json
{
  "success": true,
  "message": "Notification marked as read.",
  "data": null,
  "errors": null
}
```

---

### 2.4) `POST /api/DocumentSignatures/{id}/sign`

وقّع مستند — بيحسب `SignatureHash` بـ SHA256 ويحدّث `Status = Signed`.

**Request:**
```
POST /api/DocumentSignatures/8/sign
```
(Body فارغ)

**Response:**
```json
{
  "success": true,
  "message": "Document signed successfully.",
  "data": null,
  "errors": null
}
```

---

### 2.5) `POST /api/DocumentSignatures/{id}/reject`

ارفض توقيع مستند مع سبب الرفض.

**Request:**
```
POST /api/DocumentSignatures/8/reject
Content-Type: application/json

{
  "rejectionReason": "المستند يحتاج مراجعة فنية إضافية"
}
```

**Response:**
```json
{
  "success": true,
  "message": "Document signature rejected.",
  "data": null,
  "errors": null
}
```

---

## 👥 3) Roles Controller (`/api/Roles`)

> **ملاحظة:** كل endpoints دي محمية بـ `[Authorize(Roles = "Manager")]`.

### 3.1) `GET /api/Roles`

List كل الأدوار مع الـ Description.

**Response:**
```json
{
  "success": true,
  "message": "Retrieved successfully.",
  "data": [
    { "id": "abc123", "name": "Manager", "description": "مدير النظام" },
    { "id": "def456", "name": "Accounting", "description": "محاسب" },
    { "id": "ghi789", "name": "Hr", "description": "موارد بشرية" }
  ],
  "errors": null
}
```

### 3.2) `GET /api/Roles/lookup`

id + name بس (للـ dropdowns).

**Response:**
```json
{
  "success": true,
  "message": "Retrieved successfully.",
  "data": [
    { "id": "abc123", "name": "Manager" },
    { "id": "def456", "name": "Accounting" },
    { "id": "ghi789", "name": "Hr" }
  ],
  "errors": null
}
```

### 3.3) `POST /api/Roles`

أنشئ دور جديد.

**Request:**
```json
{
  "name": "Procurement",
  "description": "مسؤول المشتريات"
}
```

**Response:**
```json
{
  "success": true,
  "message": "Role created successfully.",
  "data": {
    "id": "jkl012",
    "name": "Procurement",
    "description": "مسؤول المشتريات"
  },
  "errors": null
}
```

### 3.4) `PUT /api/Roles/{id}`

حدّث دور موجود.

**Request:**
```
PUT /api/Roles/jkl012
Content-Type: application/json

{
  "name": "Procurement Officer",
  "description": "مسؤول المشتريات والاستيراد"
}
```

**Response:**
```json
{
  "success": true,
  "message": "Role updated successfully.",
  "data": null,
  "errors": null
}
```

### 3.5) `DELETE /api/Roles/{id}`

احذف دور — **يُمنع حذف أي دور مربوط بمستخدمين فعليًا**.

**Response (نجاح):**
```json
{
  "success": true,
  "message": "Role deleted successfully.",
  "data": null,
  "errors": null
}
```

**Response (فشل — دور مرتبط بمستخدمين):**
```json
{
  "success": false,
  "message": "لا يمكن حذف دور مرتبط بمستخدمين نشطين",
  "data": null,
  "errors": []
}
```
> Status Code: `409 Conflict`

---

## 🌱 4) Chart of Accounts Seed Data

ملف SQL جاهز للتنفيذ على قاعدة البيانات:

**المسار:** `Infrastructure/Persistence/Scripts/SeedChartOfAccounts.sql`

**طريقة الاستخدام:**
```bash
sqlcmd -S . -d PetroNexusDB -E -i Infrastructure/Persistence/Scripts/SeedChartOfAccounts.sql
```

أو افتحه في SQL Server Management Studio ونفذه.

**الحسابات اللي بيضيفها (لو مش موجودة):**

| Code  | Name (AR)                    | Type     |
|-------|------------------------------|----------|
| 1101  | نقدية / بنك                  | Asset    |
| 1102  | وديعة هامش خطاب الاعتماد     | Asset    |
| 1201  | عملاء (مدينون)               | Asset    |
| 1301  | مخزون                        | Asset    |
| 1302  | مخزون تحت التصنيع (WIP)      | Asset    |
| 2101  | موردون (دائنون)              | Liability|
| 2102  | رواتب مستحقة                 | Liability|
| 4101  | إيرادات                      | Revenue  |
| 4201  | إيرادات عمولات               | Revenue  |
| 5101  | مصروفات رواتب                | Expense  |
| 5102  | مصروفات شحن                  | Expense  |
| 5103  | مصروفات تصنيع                | Expense  |
| 5104  | تكلفة البضاعة المباعة (COGS) | Expense  |

> **مهم:** الأكواد دي مطابقة بالظبط لِلي بيتوقعها `JournalPostingService.cs` — متغيرهاش.

---

## 🧪 5) Testing

مشروع `PetroNexus.Tests` (xUnit) فيه 7 tests:

- **3 tests** على `JournalEntry.IsBalanced` (متوازن، غير متوازن، تحويل عملة)
- **4 tests** على `CreateCommissionDtoValidator` (CommissionPercentage بين 0 و100)

**التشغيل:**
```bash
dotnet test PetroNexus.sln
```

**GitHub Actions workflow:** `.github/workflows/ci.yml` — يشغّل `dotnet build && dotnet test` على كل push.

---

## 📋 ملخص كل الـ Endpoints الجديدة

| #  | Method | Endpoint                                      | الوصف                              |
|----|--------|-----------------------------------------------|------------------------------------|
| 1  | GET    | `/api/Dashboard/tender-win-rate`              | نسبة فوز المناقصات                 |
| 2  | GET    | `/api/Dashboard/expediting-overview`          | لوحة التعجيل الشاملة               |
| 3  | GET    | `/api/Dashboard/principal-profitability`      | ربحية كل Principal                 |
| 4  | GET    | `/api/Dashboard/revenue-forecast`             | توقع الإيرادات                     |
| 5  | GET    | `/api/Dashboard/financial-summary`            | ملخص مالي + أرصدة العملات          |
| 6  | GET    | `/api/Dashboard/revenue-collection-monthly`   | مفوتر مقابل محصّل (شهري)          |
| 7  | GET    | `/api/Dashboard/tender-pipeline`              | توزيع مراحل المناقصات (Pie Chart)  |
| 8  | POST   | `/api/TenderLeads/{id}/convert-to-tender`     | تحويل Lead لمناقصة                 |
| 9  | POST   | `/api/EscalationLogs/{id}/acknowledge`        | تأكيد استلام تنبيه                 |
| 10 | POST   | `/api/Notifications/{id}/mark-read`           | تحديد إشعار كمقروء                 |
| 11 | POST   | `/api/DocumentSignatures/{id}/sign`           | توقيع مستند (SHA256)               |
| 12 | POST   | `/api/DocumentSignatures/{id}/reject`         | رفض توقيع مستند                    |
| 13 | GET    | `/api/Roles`                                  | List كل الأدوار                    |
| 14 | GET    | `/api/Roles/lookup`                            | Lookup للأدوار (dropdowns)         |
| 15 | POST   | `/api/Roles`                                  | إنشاء دور                          |
| 16 | PUT    | `/api/Roles/{id}`                             | تحديث دور                          |
| 17 | DELETE | `/api/Roles/{id}`                             | حذف دور (مع منع الحذف المرتبط)     |

---

## ⚠️ ملاحظات مهمة للفرونت إند

1. **كل الـ responses** ملفوفة في `ApiResponse<T>` بنفس الشكل: `{ success, message, data, errors }`
2. **الـ Dashboard endpoints** كلها `GET` ومش محتاجة أي query parameters
3. **`tender-pipeline`** بيستخدم `"UnderPrep"` (مش `"UnderPreparation"`) عشان متطابق مع الفرونت
4. **`revenue-collection-monthly`** بيرد Array مباشر (مش object) — كل عنصر فيه `month`, `invoiced`, `collected`
5. **`tender-win-rate`** فيه حقل `newLeadsThisWeek` جديد — عدد الـ Leads اللي ظهرت في آخر 7 أيام
6. **`Roles` endpoints** محمية بـ `[Authorize(Roles = "Manager")]` — لازم تكون Manager
7. **`DocumentSignatures/reject`** محتاج body فيه `rejectionReason` (string)
8. **`DocumentSignatures/sign`** مش محتاج body — بيحسب الـ hash تلقائيًا
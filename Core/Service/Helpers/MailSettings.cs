// This file is kept for backward compatibility.
// MailSettings has been moved to Shared.Common namespace.
// Use `using Shared.Common;` instead of `using Service.Helpers;` for MailSettings.

using Shared.Common;

namespace Service.Helpers
{
    // Type-forwarding alias so any old `Service.Helpers.MailSettings` references still compile.
    // [Obsolete("Use Shared.Common.MailSettings instead.")]
}
// This file is kept for backward compatibility.
// IMailingService has been moved to ServiceAbstraction namespace.
// Use `using ServiceAbstraction;` instead of `using Service.Helpers;` for IMailingService.

using ServiceAbstraction;

namespace Service.Helpers
{
    // Type-forwarding alias so any old `Service.Helpers.IMailingService` references still compile.
    // [Obsolete("Use ServiceAbstraction.IMailingService instead.")]
}
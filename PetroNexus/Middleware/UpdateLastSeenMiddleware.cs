using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using System.Security.Claims;

namespace PetroNexus.Middleware
{
    public class UpdateLastSeenMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IMemoryCache _cache;

        // مدة منع التحديث المتكرر لنفس المستخدم
        private static readonly TimeSpan UpdateInterval = TimeSpan.FromSeconds(30);

        public UpdateLastSeenMiddleware(RequestDelegate next, IMemoryCache cache)
        {
            _next = next;
            _cache = cache;
        }

        public async Task InvokeAsync(
            HttpContext context,
            UserManager<AppUser> userManager)
        {
            if (context.User.Identity?.IsAuthenticated == true)
            {
                var userId =
                    context.User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (!string.IsNullOrEmpty(userId))
                {
                    var cacheKey = $"lastSeenUpdate_{userId}";

                    // لو لسه محدث قريب → ما تضربش الداتابيز
                    if (!_cache.TryGetValue(cacheKey, out _))
                    {
                        var user = await userManager.FindByIdAsync(userId);

                        if (user != null)
                        {
                            user.LastSeen = DateTime.UtcNow;

                            // مش ضروري نحدث IsOnline
                            // presence الحقيقي يتحسب من LastSeen

                            await userManager.UpdateAsync(user);
                        }

                        // احفظ علامة في الكاش تمنع التحديث لفترة
                        _cache.Set(
                            cacheKey,
                            true,
                            UpdateInterval);
                    }
                }
            }

            await _next(context);
        }
    }
}

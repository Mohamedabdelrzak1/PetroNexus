using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace Service.Hubs
{
    [Authorize]
    public class NotificationHub : Hub
    {
        // ============================================================
        // OnConnectedAsync — كل يوزر يدخل في group باسمه
        // ============================================================
        public override async Task OnConnectedAsync()
        {
            var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!string.IsNullOrEmpty(userId))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, userId);
            }

            await base.OnConnectedAsync();
        }

        // ============================================================
        // OnDisconnectedAsync
        // ============================================================
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!string.IsNullOrEmpty(userId))
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, userId);
            }

            await base.OnDisconnectedAsync(exception);
        }

        // ============================================================
        // MarkAsRead — من الكلاينت مباشرة
        // ============================================================
        public async Task MarkAsRead(int notificationId)
        {
            var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId)) return;

            // Broadcast للمستخدم نفسه على كل الأجهزة
            await Clients.Group(userId)
                .SendAsync("NotificationRead", notificationId);
        }

        // ============================================================
        // MarkAllAsRead
        // ============================================================
        public async Task MarkAllAsRead()
        {
            var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId)) return;

            await Clients.Group(userId)
                .SendAsync("AllNotificationsRead");
        }
    }
}

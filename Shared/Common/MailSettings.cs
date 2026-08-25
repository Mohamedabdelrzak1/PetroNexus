namespace Shared.Common
{
    /// <summary>
    /// Configuration settings for the email (SMTP) service.
    /// Moved to Shared so Infrastructure.Persistence no longer depends on Service.
    /// </summary>
    public class MailSettings
    {
        public string Email { get; set; } = null!;
        public string DisplayName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Host { get; set; } = null!;
        public int Port { get; set; }
    }
}
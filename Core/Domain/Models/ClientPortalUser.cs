using Domain.Common;

namespace Domain.Models
{
    // يربط شركة العميل (Client) بمستخدم دخول فعلي (AppUser) عشان يستخدم بوابة العميل
    public class ClientPortalUser : BaseEntity<int>
    {
        public int ClientId { get; set; }
        public Client Client { get; set; }

        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public bool IsActive { get; set; } = true;
    }
}

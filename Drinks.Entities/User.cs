using JetBrains.Annotations;

namespace Drinks.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public byte[] Password { get; set; }
        public byte[] Salt { get; set; }
        public bool IsAdmin { get; set; }
        public string BadgeId { get; set; }
        public int DiscountPercentage { get; set; }

        public User() { }

        public User([NotNull] string name, [NotNull] string username, [NotNull] string badgeId, bool isAdmin)
        {
            Name = name;
            Username = username;
            BadgeId = badgeId;
            IsAdmin = isAdmin;
        }
    }
}
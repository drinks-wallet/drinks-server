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
        public UserPermissions Permissions { get; set; }
        public string BadgeId { get; set; }

        public User() { }

        public User([NotNull] string name, [NotNull] string username, [NotNull] string badgeId, UserPermissions permissions)
        {
            Name = name;
            Username = username;
            BadgeId = badgeId;
            Permissions = permissions;
        }
    }
}
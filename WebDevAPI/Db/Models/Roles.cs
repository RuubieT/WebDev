namespace WebDevAPI.Db.Models
{
    public class Roles
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class UserRoles
    {
        public User User { get; set; }
        public Guid UserId { get; set; }
        public Roles Roles { get; set; }
        public Guid RoleId { get; set; }
    }
}

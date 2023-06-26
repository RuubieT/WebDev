namespace WebDevAPI.Db.Dto_s.User
{
    public class GetUserRoleDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public IList<string> Role { get; set; }
    }
}

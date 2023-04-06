namespace WebDevAPI.Db.Dto_s.Player
{
    public class GetChangePasswordDto
    {
        public string Email { get; set; }
    }

    public class PostChangePasswordDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }
}

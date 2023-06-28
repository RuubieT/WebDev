namespace WebDevAPI.Db.Dto_s.Player
{
    public class GetChangePasswordDto
    {
        public string Email { get; set; }
    }

    public class PutForgotPasswordDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }

    public class PutChangePasswordDto
    {
        public string Email { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}

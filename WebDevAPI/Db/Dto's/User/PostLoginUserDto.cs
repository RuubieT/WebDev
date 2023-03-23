using System.ComponentModel.DataAnnotations;

namespace WebDevAPI.Db.Dto_s.User
{
    public class PostLoginUserDto
    {
        [Required] public string Email { get; set; }
        [Required] public string Password { get; set; }
    }
}

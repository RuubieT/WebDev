using System.ComponentModel.DataAnnotations;

namespace WebDevAPI.Db.Dto_s.User
{
    public class PostRegisterUserDto
    {
        [Required] public string FirstName { get; set; }
        [Required] public string LastName { get; set; }
        [Required] public string Email { get; set; }
        [Required] public string Password { get; set; }
    }
}

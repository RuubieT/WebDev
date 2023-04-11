using System.ComponentModel.DataAnnotations;

namespace WebDevAPI.Db.Dto_s.User
{
    public class PostGAuthCodeDto
    {
        [Required] public string Email { get; set; }
        [Required] public string Code { get; set; }
    }
}

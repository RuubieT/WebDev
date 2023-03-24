using WebDevAPI.Db.Dto_s.User;
using WebDevAPI.Db.Models;

namespace WebDevAPI.Db.Dto_s.Player
{
    public class PostPlayerDto : PostLoginUserDto
    {
        public string Username { get; set; }
    }
}

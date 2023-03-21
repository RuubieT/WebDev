using WebDevAPI.Db.Dto_s.User;
using WebDevAPI.Db.Models;

namespace WebDevAPI.Db.Dto_s.Player
{
    public class PostPlayerDto : PostUserDto
    {
        public string Username { get; set; }
        public int Chips { get; set; }
    }
}
